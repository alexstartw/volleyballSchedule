namespace VolleyBallSchedule.Models;

public class TempPlayerList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Gender { get; set; } // 0: 女, 1: 男
    public string InviteLineId { get; set; }
    public DateTime UpdatedTime { get; set; }
}