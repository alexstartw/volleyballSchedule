using VolleyBallSchedule.Models.Requests;

namespace VolleyBallSchedule.Services;

public interface ILineBotService
{
    public void ReceiveWebhook(WebhookRequestBodyDto requestBody);
}