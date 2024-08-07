namespace VolleyBallSchedule.Models;

public class GroupPlayers
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Gender { get; set; } // 0: 女, 1: 男
    public int Status { get; set; }
    public string LineId { get; set; }
    public DateTime CreatedTime { get; set; }
}