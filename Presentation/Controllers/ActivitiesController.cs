using Application.Contracts.Activities;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("/activities")]
public class ActivitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ActivitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public CancellationToken CancellationToken => HttpContext.RequestAborted;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAsync()
    {
        var command = new GetActivities.Command();
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Activities);
    }
}