using System.Net;
using MediatR;
using VolleyBallSchedule.Enum;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Services;

public class AddAbsentRecordHandler : IRequestHandler<AddAbsentRecordRequest, ApiResult>
{
    private readonly IGroupPlayerRepo _groupPlayerRepo;
    private readonly IAttendingListRepo _attendingListRepo;
    private readonly ILogger<AddAbsentRecordHandler> _logger;

    public AddAbsentRecordHandler(IGroupPlayerRepo groupPlayerRepo, IAttendingListRepo attendingListRepo,
        ILogger<AddAbsentRecordHandler> logger)
    {
        _groupPlayerRepo = groupPlayerRepo;
        _attendingListRepo = attendingListRepo;
        _logger = logger;
    }

    public Task<ApiResult> Handle(AddAbsentRecordRequest request, CancellationToken cancellationToken)
    {
        var activityTime = _attendingListRepo.GetActivityTime();
        if (request.Message.Contains(MessageKeywordEnum.AttendToAbsent))
        {
            // 確認是否為季打
            var player = _groupPlayerRepo.GetPlayer(request.LineId).Result;
            if (player == default)
            {
                _logger.LogInformation($"LineId: {request.LineId} is not a season player");
                return Task.FromResult(new ApiResult()
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Msg = "非季打成員"
                });
            }

            // 確認是否出席
            if (_attendingListRepo.CheckIfAttended(player.LineId, activityTime))
            {
                // 修改出席紀錄為缺席
                var attendRecord = _attendingListRepo.GetAttendingList(request.LineId, activityTime);

                attendRecord.AttendingStatus = (int)AttendListStatusEnum.Cancel;
                attendRecord.UpdateTime = DateTimeOffset.Now.DateTime;
                if (_attendingListRepo.UpdateAttendRecord(attendRecord) == 1)
                {
                    _logger.LogInformation($"LineId: {request.LineId} Update attend record to absent successfully");
                    return Task.FromResult(new ApiResult()
                    {
                        Code = (int)HttpStatusCode.OK,
                        Msg = "修改成功"
                    });
                }

                _logger.LogInformation($"LineId: {request.LineId} Update attend record to absent failed");
                return Task.FromResult(new ApiResult()
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Msg = "修改失敗"
                });
            }
        }

        // 直接走請假流程
        if (_attendingListRepo.AddAbsentRecord(request.LineId, activityTime) == 1)
        {
            _logger.LogInformation($"LineId: {request.LineId} Add absent record successfully");
            return Task.FromResult(new ApiResult()
            {
                Code = (int)HttpStatusCode.OK,
                Msg = "請假成功"
            });
        }
        
        _logger.LogInformation($"LineId: {request.LineId} Add absent record failed");
        return Task.FromResult(new ApiResult()
        {
            Code = (int)HttpStatusCode.InternalServerError,
            Msg = "請假失敗"
        });
    }
}