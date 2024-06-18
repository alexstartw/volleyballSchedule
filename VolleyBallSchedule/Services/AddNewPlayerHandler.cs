using MediatR;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Services;

public class AddNewPlayerHandler : IRequestHandler<AddNewPlayerRequest, ApiResult>
{
    private readonly IPlayerRepo _playerRepo;
    private readonly ILogger<AddNewPlayerHandler> _logger;

    public AddNewPlayerHandler(IPlayerRepo playerRepo, ILogger<AddNewPlayerHandler> logger)
    {
        _playerRepo = playerRepo;
        _logger = logger;
    }

    public async Task<ApiResult> Handle(AddNewPlayerRequest request, CancellationToken cancellationToken)
    {
        var player = new Players
        {
            Name = request.Name,
            NickName = request.NickName
        };

        try
        {
            if (player.Name == null || player.NickName == null)
            {
                _logger.LogError("Name and Nickname cannot be null");
                throw new Exception("Name and Nickname cannot be null");
            }

            var playerExist = await _playerRepo.CheckPlayerExist(player.Name);
            if (playerExist)
            {
                _logger.LogError("Player already exist");
                return new ApiFailedResult(code: 400, msg: $"Player {player.Name} already exist");
            }

            var updateLine = await _playerRepo.AddPlayer(player);
            if (updateLine > 0)
                return new ApiResult { Msg = "Player added successfully" };
            return new ApiFailedResult(code: 500, msg: "Failed to add player");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ApiFailedResult(code: 500, msg: e.Message);
        }
    }
}