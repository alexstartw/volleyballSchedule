using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.Repos.Interfaces;

public interface IAttendingListRepo
{
    Task<int> AddAttendRecord(string lineId, int gender, DateTime activityTime);
    bool CheckIfAttended(string lineId, DateTime activityTime);
    AttendingList GetAttendingList(string lineId, DateTime activityTime);
    int UpdateAttendRecord(AttendingList attendingList);
    int AddAbsentRecord(string lineId, DateTime activityTime);
    DateTime GetActivityTime();
}