using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.Repos.Interfaces;

public interface ITempPlayerListRepo
{
    bool CheckTempPlayerExist(string name);
    int AddTempPlayer(TempPlayerList tempPlayer);
}