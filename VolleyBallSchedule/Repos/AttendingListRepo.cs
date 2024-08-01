using VolleyBallSchedule.Models;
using VolleyBallSchedule.Repos.Interfaces;

namespace VolleyBallSchedule.Repos;

public class AttendingListRepo : IAttendingListRepo
{
    private readonly PlayerContext _context;

    public AttendingListRepo(PlayerContext context)
    {
        _context = context;
    }
    
    // 新增季打出席紀錄
    public Task<int> AddAttendRecord(string lineId, int gender, DateTime activityTime)
    {
        if (CheckIfAttended(lineId, activityTime))
            return Task.FromResult(0);
        
        var attendingList = new AttendingList
        {
            LineId = lineId,
            Gender = gender,
            ActivityTime = activityTime,
            UpdateTime = DateTimeOffset.Now.DateTime
        };
        
        _context.AttendingList.Add(attendingList);
        return _context.SaveChangesAsync();
    }
    
    // 新增臨打出席紀錄
    public void AddInterimAttendRecord(string interimId, DateTime activityTime)
    {
        
        
    }
    
    // 確認是否已經填過出席
    public bool CheckIfAttended(string lineId, DateTime activityTime)
    {
        return _context.AttendingList.Any(x => x.LineId == lineId && x.ActivityTime == activityTime);
    }
}

