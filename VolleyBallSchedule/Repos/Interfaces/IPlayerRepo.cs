using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.Repos.Interfaces;

public interface IPlayerRepo
{
    Task<int> AddPlayer(Players player);
    Task<bool> CheckPlayerExist(string name);
}