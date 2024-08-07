using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.Repos.Interfaces;

public interface IGroupPlayerRepo
{
    Task<int> AddPlayer(GroupPlayers? player);
    Task<GroupPlayers> GetPlayer(string lineId);
    bool CheckIfPlayerExists(string lineId);
}