using VolleyBallSchedule.Enum;
using VolleyBallSchedule.Models.Requests;

namespace VolleyBallSchedule.Services;

public class LineBotService : ILineBotService
{
    private readonly string channelAccessToken = "Nymkrl34iylj+EFYqahkp3RW+7fdmWs1fywCoO2gcPfGbX31H9JMtBI1W6VUW7AVir/pdv+LmX239+mt//xoYyu3r0DctBOMbQQzOonZPIm9d4q9Rb2cX5XkaaQSjdXKSeA4ef0GbsuVogdF4c6tCgdB04t89/1O/w1cDnyilFU=";
    private readonly string channelSecret = "5d469dd2851c6ea2de6ce57c543fff07";
    private readonly ILogger<LineBotService> _logger;
    
    public LineBotService(ILogger<LineBotService> logger)
    {
        _logger = logger;
    }
    
    public void ReceiveWebhook(WebhookRequestBodyDto requestBody)
    {
        foreach (var obj in requestBody.Events)
        {
            switch (obj.Type)
            {
                case WebhookEventTypeEnum.Message:
                    _logger.LogInformation("Message event");
                    break;
                case WebhookEventTypeEnum.Unsend:
                    _logger.LogInformation(@"User {obj.Source.UserId} unsend message");
                    break;
                case WebhookEventTypeEnum.Follow:
                    _logger.LogInformation(@"User {obj.Source.UserId} follow");
                    break;
                case WebhookEventTypeEnum.Unfollow:
                    _logger.LogInformation(@"User {obj.Source.UserId} unfollow");
                    break;
                case WebhookEventTypeEnum.Join:
                    _logger.LogInformation(@"User {obj.Source.UserId} join");
                    break; 
                case WebhookEventTypeEnum.Leave:
                    _logger.LogInformation(@"bot leave the chat");
                    break;
            }
        }
    }
    

}