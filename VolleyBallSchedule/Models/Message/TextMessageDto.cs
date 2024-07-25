using VolleyBallSchedule.Enum;

namespace VolleyBallSchedule.Models.Message;

public class TextMessageDto : BaseMessageDto
{
    public TextMessageDto()
    {
        Type = MessageTypeEnum.Text;
    }
    public string Text { get; set; }
}