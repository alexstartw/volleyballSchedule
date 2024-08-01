using MediatR;

namespace VolleyBallSchedule.Models.Requests;

public class CreateSeasonPlayerRequest : IRequest<ApiResult>
{
    public string Name { get; set; }
    public int Gender { get; set; }
    public int Status { get; set; }
    public string LineId { get; set; }
}