using MediatR;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Services;

public class EditPlayerRequestHandler : IRequestHandler<EditPlayerRequest, ApiResult>
{
    private readonly IPlayerRepo _playerRepo;
    private readonly ILogger<EditPlayerRequestHandler> _logger;
        
    public EditPlayerRequestHandler(IPlayerRepo playerRepo, ILogger<EditPlayerRequestHandler> logger)
    {
        _playerRepo = playerRepo;
        _logger = logger;
    }

    public async Task<ApiResult> Handle(EditPlayerRequest request, CancellationToken cancellationToken)
    {
        var player = _playerRepo.GetPlayerById(request.Id);
        if (player == default)
        {
            _logger.LogError("Player not found");
            return new ApiFailedResult(code: 400, msg: "Player not found");
        }
        
        if (!string.IsNullOrEmpty(request.Name))
            player.Name = request.Name;
        if (!string.IsNullOrEmpty(request.NickName))
            player.NickName = request.NickName;
        if (request.Gender != 0)
            player.Gender = request.Gender;
        
        _playerRepo.UpdatePlayer(player);
        return new ApiResult(){Msg = "Player updated successfully"};
    }
}