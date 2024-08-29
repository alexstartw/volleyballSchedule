using MediatR;

namespace VolleyBallSchedule.Models.Requests;

public class VerifyTempPlayerRequest : IRequest<ApiResult>
{
    public string InviteLineId { get; set; }
    public string TempPlayerName { get; set; }
    public int TempPlayerGender { get; set; }
}