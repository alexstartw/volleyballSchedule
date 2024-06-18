using MediatR;

namespace VolleyBallSchedule.Models.Requests;

public class AddNewPlayerRequest : IRequest<ApiResult>
{
    public string Name { get; set; }
    public string NickName { get; set; }
}