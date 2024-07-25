using Microsoft.AspNetCore.Mvc;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Services;

namespace VolleyBallSchedule.Controller;

[ApiController]
[Route("api/[controller]")]
public class LineBotController : ControllerBase
{
    private readonly string channelAccessToken = "Nymkrl34iylj+EFYqahkp3RW+7fdmWs1fywCoO2gcPfGbX31H9JMtBI1W6VUW7AVir/pdv+LmX239+mt//xoYyu3r0DctBOMbQQzOonZPIm9d4q9Rb2cX5XkaaQSjdXKSeA4ef0GbsuVogdF4c6tCgdB04t89/1O/w1cDnyilFU=";
    private readonly string channelSecret = "5d469dd2851c6ea2de6ce57c543fff07";

    private readonly ILineBotService _lineBotService;
    public LineBotController(ILineBotService lineBotService)
    {
        _lineBotService = lineBotService;
    }

    [HttpPost("webhook")]
    public IActionResult Webhook(WebhookRequestBodyDto body)
    {
        _lineBotService.ReceiveWebhook(body);
        return Ok();
    }
}