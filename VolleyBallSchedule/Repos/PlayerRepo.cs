using Microsoft.EntityFrameworkCore;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Repos;

public class PlayerRepo : IPlayerRepo
{
    private readonly PlayerContext _context;

    public PlayerRepo(PlayerContext context)
    {
        _context = context;
    }
    
    public async Task<int> AddPlayer(Players player)
    {
        await _context.Players.AddAsync(player);
        await _context.SaveChangesAsync();
        return player.Id;
    }
    
    public async Task<bool> CheckPlayerExist(string name)
    {
        var player = await _context.Players.FirstOrDefaultAsync(x => x.Name == name);
        return player != null;
    }
}