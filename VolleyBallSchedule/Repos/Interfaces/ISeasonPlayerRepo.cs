using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.Repos.Interfaces;

public interface ISeasonPlayerRepo
{
    Task<int> AddPlayer(SeasonPlayers player);
}