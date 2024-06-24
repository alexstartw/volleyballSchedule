using MediatR;

namespace VolleyBallSchedule.Models.Requests;

public class EditPlayerRequest : IRequest<ApiResult>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NickName { get; set; }
    public int Gender { get; set; }
}