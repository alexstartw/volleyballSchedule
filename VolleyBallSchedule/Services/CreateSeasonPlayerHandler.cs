using System.Net;
using MediatR;
using VolleyBallSchedule.Enum;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Services;

public class CreateSeasonPlayerHandler : IRequestHandler<CreateSeasonPlayerRequest, ApiResult>
{
    private readonly IGroupPlayerRepo _groupPlayerRepo;
    private readonly ILogger<CreateSeasonPlayerHandler> _logger;

    public CreateSeasonPlayerHandler(IGroupPlayerRepo groupPlayerRepo, ILogger<CreateSeasonPlayerHandler> logger)
    {
        _groupPlayerRepo = groupPlayerRepo;
        _logger = logger;
    }

    public Task<ApiResult> Handle(CreateSeasonPlayerRequest request, CancellationToken cancellationToken)
    {
        var seasonPlayer = new GroupPlayers
        {
            Name = request.Name,
            Gender = request.Gender,
            Status = (int)GroupPlayerEnum.SeasonPlayer,
            LineId = request.LineId,
            CreatedTime = DateTimeOffset.Now.DateTime
        };
        
        var result = _groupPlayerRepo.AddPlayer(seasonPlayer).Result;
        if (result == 0)
        {
            _logger.LogInformation($"Player {seasonPlayer.Name} already exists");
            return Task.FromResult(new ApiResult
            {
                Code = (int)HttpStatusCode.Conflict,
                Msg = "Player already exists"
            });
        }
        
        _logger.LogInformation($"Player {seasonPlayer.Name} created successfully");
        return Task.FromResult(new ApiResult
        {
            Code = (int)HttpStatusCode.OK,
            Msg = "Player created successfully"
        });
    }
}