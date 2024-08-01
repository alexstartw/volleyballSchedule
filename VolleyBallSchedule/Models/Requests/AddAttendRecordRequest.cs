using MediatR;

namespace VolleyBallSchedule.Models.Requests;

public class AddAttendRecordRequest : IRequest<ApiResult>
{
    public string LineId { get; set; }
}