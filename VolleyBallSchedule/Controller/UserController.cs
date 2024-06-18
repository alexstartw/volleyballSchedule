using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolleyBallSchedule.Models;
using VolleyBallSchedule.Models.Requests;
using VolleyBallSchedule.Services;

namespace VolleyBallSchedule.Controller;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;


    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpPost("add")]
    public async Task<IActionResult> Add(string name, string nickname)
    {
        var request = new AddNewPlayerRequest
        {
            Name = name,
            NickName = nickname
        };
        
        var response = await _mediator.Send(request);
        return StatusCode(response.Code, response);
    }
}