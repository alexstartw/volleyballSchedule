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
    public async Task<IActionResult> Add(string name, string nickname, int gender)
    {
        var request = new AddNewPlayerRequest
        {
            Name = name,
            NickName = nickname,
            Gender = gender
        };
        
        var response = await _mediator.Send(request);
        return StatusCode(response.Code, response);
    }

    [AllowAnonymous]
    [HttpPost("edit")]
    public async Task<IActionResult> Edit(int id, string name = "", string nickname = "", int gender = 0)
    {
        var request = new EditPlayerRequest
        {
            Id = id,
            Name = name,
            NickName = nickname,
            Gender = gender
        };
        
        var response = await _mediator.Send(request);
        return StatusCode(response.Code, response);
    }
    
    [AllowAnonymous]
    [HttpGet("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var request = new DeletePlayerRequest
        {
            Id = id
        };
        
        var response = await _mediator.Send(request);
        return StatusCode(response.Code, response);
    }
}