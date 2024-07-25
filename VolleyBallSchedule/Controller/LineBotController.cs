using Microsoft.AspNetCore.Mvc;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Services;

namespace VolleyBallSchedule.Controller;

[ApiController]
[Route("api/[controller]")]
public class LineBotController : ControllerBase
{
    
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