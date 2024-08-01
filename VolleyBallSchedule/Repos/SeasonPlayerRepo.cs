using Microsoft.EntityFrameworkCore;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Repos;

public class SeasonPlayerRepo : ISeasonPlayerRepo
{
    private readonly PlayerContext _context;

    public SeasonPlayerRepo(PlayerContext context)
    {
        _context = context;
    }
    
    public async Task<int> AddPlayer(SeasonPlayers player)
    {
        if (CheckIfPlayerExists(player.LineId))
            return 0;
        _context.SeasonPlayers.Add(player);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<SeasonPlayers> GetPlayer(string lineId)
    {
        var player = await _context.SeasonPlayers.FirstOrDefaultAsync(e => e.LineId == lineId);
        return player ?? default;
    }
 
    public bool CheckIfPlayerExists(string lineId)
    {
        return _context.SeasonPlayers.Any(e => e.LineId == lineId);
    }
}