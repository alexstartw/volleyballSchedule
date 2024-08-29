using VolleyBallSchedule.Models;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Repos;

public class TempPlayerListRepo : ITempPlayerListRepo
{
    private readonly PlayerContext _context;

    public TempPlayerListRepo(PlayerContext context)
    {
        _context = context;
    }

    public bool CheckTempPlayerExist(string name)
    {
        return _context.TempPlayerList.Any(x => x.Name == name);
    }
    
    public int AddTempPlayer(TempPlayerList tempPlayer)
    {
        _context.TempPlayerList.Add(tempPlayer);
        return _context.SaveChanges();
    }
}