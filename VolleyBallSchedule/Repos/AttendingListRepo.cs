using VolleyBallSchedule.Enum;
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
            UpdateTime = DateTimeOffset.Now.DateTime,
            AttendingStatus = (int)AttendListStatusEnum.Available
        };
        
        _context.AttendingList.Add(attendingList);
        return _context.SaveChangesAsync();
    }
    
    public int AddAbsentRecord(string lineId, DateTime activityTime)
    {
        if (CheckIfAttended(lineId, activityTime))
            return 0;
        
        var attendingList = new AttendingList
        {
            LineId = lineId,
            ActivityTime = activityTime,
            UpdateTime = DateTimeOffset.Now.DateTime,
            AttendingStatus = (int)AttendListStatusEnum.Cancel
        };
        
        _context.AttendingList.Add(attendingList);
        return _context.SaveChanges();
    }
    
    // 新增臨打出席紀錄
    public void AddInterimAttendRecord(string interimId, DateTime activityTime)
    {
        
        
    }
    
    public AttendingList GetAttendingList(string lineId, DateTime activityTime)
    {
        return _context.AttendingList.FirstOrDefault(x => x.LineId == lineId && x.ActivityTime == activityTime);
    }
    
    public int UpdateAttendRecord(AttendingList attendingList)
    {
        _context.AttendingList.Update(attendingList);
        return _context.SaveChanges();
    }
    
    // 確認是否已經填過出席
    public bool CheckIfAttended(string lineId, DateTime activityTime)
    {
        return _context.AttendingList.Any(x => x.LineId == lineId && x.ActivityTime == activityTime);
    }

    public DateTime GetActivityTime()
    {
        var today = DateTimeOffset.Now;
        var daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
        daysUntilNextMonday = daysUntilNextMonday == 0 ? 7 : daysUntilNextMonday; // 如果今天是星期一，则获取下一个星期一
        var nextMonday = today.AddDays(daysUntilNextMonday);
        return nextMonday.DateTime.Date;
    }
}

