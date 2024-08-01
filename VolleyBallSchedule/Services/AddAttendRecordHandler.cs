using System.Net;
using MediatR;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Services;

public class AddAttendRecordHandler : IRequestHandler<AddAttendRecordRequest, ApiResult>
{
    private readonly ISeasonPlayerRepo _seasonPlayerRepo;
    private readonly IAttendingListRepo _attendingListRepo;

    public AddAttendRecordHandler(IAttendingListRepo attendingListRepo, ISeasonPlayerRepo seasonPlayerRepo)
    {
        _attendingListRepo = attendingListRepo;
        _seasonPlayerRepo = seasonPlayerRepo;
    }

    public Task<ApiResult> Handle(AddAttendRecordRequest request, CancellationToken cancellationToken)
    {
        // 新增出席紀錄
        var seasonPlayer = _seasonPlayerRepo.GetPlayer(request.LineId).Result;
        if (seasonPlayer == default)
            return Task.FromResult(new ApiResult
            {
                Code = (int)HttpStatusCode.NotFound,
                Msg = "尚未註冊季打球員"
            });
        
        var result = _attendingListRepo.AddAttendRecord(seasonPlayer.LineId, seasonPlayer.Gender, GetActivityTime()).Result;
        if (result == 0)
            return Task.FromResult(new ApiResult
            {
                Code = (int)HttpStatusCode.Conflict,
                Msg = seasonPlayer.Name + "已經填過了!"
            });
        
        return Task.FromResult(new ApiResult
        {
            Code = (int)HttpStatusCode.OK,
            Msg = seasonPlayer.Name + "出席成功"
        });
    }
    
    private DateTime GetActivityTime()
    {
        var today = DateTimeOffset.Now;
        var daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
        daysUntilNextMonday = daysUntilNextMonday == 0 ? 7 : daysUntilNextMonday; // 如果今天是星期一，则获取下一个星期一
        var nextMonday = today.AddDays(daysUntilNextMonday);
        return nextMonday.DateTime.Date;
    }
}