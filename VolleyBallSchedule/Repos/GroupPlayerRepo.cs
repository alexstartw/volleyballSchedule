using Microsoft.EntityFrameworkCore;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Repos;

public class GroupPlayerRepo : IGroupPlayerRepo
{
    private readonly PlayerContext _context;

    public GroupPlayerRepo(PlayerContext context)
    {
        _context = context;
    }
    
    public async Task<int> AddPlayer(GroupPlayers player)
    {
        if (CheckIfPlayerExists(player.LineId))
            return 0;
        _context.SeasonPlayers.Add(player);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<GroupPlayers> GetPlayer(string lineId)
    {
        var player = await _context.SeasonPlayers.FirstOrDefaultAsync(e => e.LineId == lineId);
        return player ?? default;
    }
 
    public bool CheckIfPlayerExists(string lineId)
    {
        return _context.SeasonPlayers.Any(e => e.LineId == lineId);
    }
}