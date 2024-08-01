using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.Repos.Interfaces;

public interface ISeasonPlayerRepo
{
    Task<int> AddPlayer(SeasonPlayers? player);
    Task<SeasonPlayers> GetPlayer(string lineId);
    bool CheckIfPlayerExists(string lineId);
}