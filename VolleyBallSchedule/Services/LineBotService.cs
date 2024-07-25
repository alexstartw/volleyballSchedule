using System.Net.Http.Headers;
using System.Text;
using VolleyBallSchedule.Enum;
using VolleyBallSchedule.Models.Message;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Provider;

namespace VolleyBallSchedule.Services;

public class LineBotService : ILineBotService
{
    private readonly ILogger<LineBotService> _logger;
    private readonly IConfiguration _configuration;

    private readonly string replyMessageUri = "https://api.line.me/v2/bot/message/reply";
    private readonly string broadcastMessageUri = "https://api.line.me/v2/bot/message/broadcast";
    private static HttpClient client = new HttpClient(); // 負責處理HttpRequest
    private readonly JsonProvider _jsonProvider = new JsonProvider();

    public LineBotService(ILogger<LineBotService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void ReceiveWebhook(WebhookRequestBodyDto requestBody)
    {
        foreach (var obj in requestBody.Events)
        {
            switch (obj.Type)
            {
                case WebhookEventTypeEnum.Message:
                    var replyMessage = new ReplyMessageRequestDto<TextMessageDto>
                    {
                        ReplyToken = obj.ReplyToken,
                        Messages = new List<TextMessageDto>
                        {
                            new TextMessageDto
                            {
                                Text = "Hello, this is your message" + obj.Message.Text
                            }
                        }
                    };
                    ReplyMessageHandler("text", replyMessage);
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

    /// <summary>
    /// 接收到回覆請求時，在將請求傳至 Line 前多一層處理(目前為預留)
    /// </summary>
    /// <param name="messageType"></param>
    /// <param name="requestBody"></param>
    public void ReplyMessageHandler<T>(string messageType, ReplyMessageRequestDto<T> requestBody)
    {
        ReplyMessage(requestBody);
    }

    /// <summary>
    /// 將回覆訊息請求送到 Line
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>    
    public async void ReplyMessage<T>(ReplyMessageRequestDto<T> request)
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _configuration.GetValue<string>("LineBot:ChannelAccessToken")); //帶入 channel access token
        var json = _jsonProvider.Serialize(request);
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(replyMessageUri),
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var response = await client.SendAsync(requestMessage);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}