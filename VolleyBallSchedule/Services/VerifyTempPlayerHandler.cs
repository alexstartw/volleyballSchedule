using System.Net;
using MediatR;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Services;

public class VerifyTempPlayerHandler : IRequestHandler<VerifyTempPlayerRequest, ApiResult>
{
    private readonly ITempPlayerListRepo _tempPlayerListRepo;

    public VerifyTempPlayerHandler(ITempPlayerListRepo tempPlayerListRepo)
    {
        _tempPlayerListRepo = tempPlayerListRepo;
    }

    public Task<ApiResult> Handle(VerifyTempPlayerRequest request, CancellationToken cancellationToken)
    {
        if (_tempPlayerListRepo.CheckTempPlayerExist(request.TempPlayerName))
        {
            return Task.FromResult(new ApiResult
            {
                Code = (int)HttpStatusCode.Conflict,
                Msg = "此臨打名稱已存在"
            });
        }

        var tempPlayer = new TempPlayerList
        {
            Name = request.TempPlayerName,
            Gender = request.TempPlayerGender,
            InviteLineId = request.InviteLineId,
            UpdatedTime = DateTimeOffset.Now.DateTime
        };

        if (_tempPlayerListRepo.AddTempPlayer(tempPlayer) == 0)
        {
            return Task.FromResult(new ApiResult
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Msg = "新增臨打名單失敗"
            });
        }
        return Task.FromResult(new ApiResult
        {
            Code = (int)HttpStatusCode.OK,
            Msg = "新增臨打名單成功"
        });
    }
}