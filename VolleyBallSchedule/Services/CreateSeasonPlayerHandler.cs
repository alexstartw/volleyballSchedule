using System.Net;
using MediatR;
using VolleyBallSchedule.Enum;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Services;

public class CreateSeasonPlayerHandler : IRequestHandler<CreateSeasonPlayerRequest, ApiResult>
{
    private readonly ISeasonPlayerRepo _seasonPlayerRepo;
    private readonly ILogger<CreateSeasonPlayerHandler> _logger;

    public CreateSeasonPlayerHandler(ISeasonPlayerRepo seasonPlayerRepo, ILogger<CreateSeasonPlayerHandler> logger)
    {
        _seasonPlayerRepo = seasonPlayerRepo;
        _logger = logger;
    }

    public Task<ApiResult> Handle(CreateSeasonPlayerRequest request, CancellationToken cancellationToken)
    {
        var seasonPlayer = new SeasonPlayers
        {
            Name = request.Name,
            Gender = request.Gender,
            Status = (int)SeasonPlayerEnum.Active,
            LineId = request.LineId
        };
        
        var result = _seasonPlayerRepo.AddPlayer(seasonPlayer).Result;
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