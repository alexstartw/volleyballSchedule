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

    public Players GetPlayerById(int id)
    {
        return _context.Players.FirstOrDefault(x => x.Id == id);
    }
    
    public int UpdatePlayer(Players player)
    {
        _context.Players.Update(player);
        return _context.SaveChanges();
    }
    
    public int DeletePlayer(int id)
    {
        var player = _context.Players.FirstOrDefault(x => x.Id == id);
        if (player == null)
            return 0;
        _context.Players.Remove(player);
        return _context.SaveChanges();
    }
}