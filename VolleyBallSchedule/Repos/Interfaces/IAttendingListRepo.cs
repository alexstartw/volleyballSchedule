namespace VolleyBallSchedule.Repos.Interfaces;

public interface IAttendingListRepo
{
    Task<int> AddAttendRecord(string lineId, int gender, DateTime activityTime);
}