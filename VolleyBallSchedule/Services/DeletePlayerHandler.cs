using MediatR;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Services;

public class DeletePlayerHandler : IRequestHandler<DeletePlayerRequest, ApiResult>
{
    private readonly IPlayerRepo _playerRepo;
    private readonly ILogger<DeletePlayerHandler> _logger;

    public DeletePlayerHandler(IPlayerRepo playerRepo, ILogger<DeletePlayerHandler> logger)
    {
        _playerRepo = playerRepo;
        _logger = logger;
    }

    public Task<ApiResult> Handle(DeletePlayerRequest request, CancellationToken cancellationToken)
    {
        var player = _playerRepo.GetPlayerById(request.Id);
        if (player == null)
        {
            _logger.LogError("Player not found");
            return Task.FromResult(new ApiResult() { Msg = "Player not found", Code = 404 });
        }

        var updateLine = _playerRepo.DeletePlayer(request.Id);
        if (updateLine > 0)
            return Task.FromResult(new ApiResult { Msg = "Player deleted successfully" });
        return Task.FromResult(new ApiResult() { Msg = "Failed to delete player", Code = 500 });
    }
}