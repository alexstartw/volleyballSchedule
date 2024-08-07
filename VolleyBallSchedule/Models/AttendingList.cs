namespace VolleyBallSchedule.Models;

public class AttendingList
{
    public int Id { get; set; }
    public string LineId { get; set; }
    public int InterimId { get; set; }
    public int Gender { get; set; }
    public int AttendingStatus { get; set; }
    public DateTime ActivityTime { get; set; }
    public DateTime UpdateTime { get; set; }
}