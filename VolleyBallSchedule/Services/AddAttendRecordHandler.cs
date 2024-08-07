using System.Net;
using MediatR;
using VolleyBallSchedule.Enum;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Services;

public class AddAttendRecordHandler : IRequestHandler<AddAttendRecordRequest, ApiResult>
{
    private readonly IGroupPlayerRepo _groupPlayerRepo;
    private readonly IAttendingListRepo _attendingListRepo;

    public AddAttendRecordHandler(IAttendingListRepo attendingListRepo, IGroupPlayerRepo groupPlayerRepo)
    {
        _attendingListRepo = attendingListRepo;
        _groupPlayerRepo = groupPlayerRepo;
    }

    public Task<ApiResult> Handle(AddAttendRecordRequest request, CancellationToken cancellationToken)
    {
        // 新增出席紀錄
        var groupPlayers = _groupPlayerRepo.GetPlayer(request.LineId).Result;
        if (groupPlayers == default)
            return Task.FromResult(new ApiResult
            {
                Code = (int)HttpStatusCode.NotFound,
                Msg = "尚未登記資料"
            });
        if (groupPlayers.Status == (int)GroupPlayerEnum.NormalPlayer)
            return Task.FromResult(new ApiResult
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Msg = "非季打成員，請以臨打方式報名"
            });

        var result = _attendingListRepo.AddAttendRecord(groupPlayers.LineId, groupPlayers.Gender, _attendingListRepo.GetActivityTime()).Result;
        if (result == 0)
            return Task.FromResult(new ApiResult
            {
                Code = (int)HttpStatusCode.Conflict,
                Msg = groupPlayers.Name + "已經填過了!"
            });
        
        return Task.FromResult(new ApiResult
        {
            Code = (int)HttpStatusCode.OK,
            Msg = groupPlayers.Name + "出席成功"
        });
    }

}