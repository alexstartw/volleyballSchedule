using MediatR;

namespace VolleyBallSchedule.Models.Requests;

public class AddAbsentRecordRequest : IRequest<ApiResult>
{
    public string LineId { get; set; }
    public string Message { get; set; }
}