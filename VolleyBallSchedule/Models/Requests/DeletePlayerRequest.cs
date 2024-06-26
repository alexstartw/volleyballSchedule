using MediatR;

namespace VolleyBallSchedule.Models.Requests;

public class DeletePlayerRequest : IRequest<ApiResult>
{
    public int Id { get; set; }
}