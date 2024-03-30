using Application.Contracts.Applications;
using Application.Contracts.Users;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.Controllers;

[ApiController]
[Route("/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly JsonSerializerSettings _jsonSettings;

    public UsersController(IMediator mediator, JsonSerializerSettings jsonSettings)
    {
        _mediator = mediator;
        _jsonSettings = jsonSettings;
    }
    
    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpGet("{userId:guid}/currentapplication")]
    public async Task<ActionResult<ApplicationDto>> GetAsync(Guid userId)
    {
        var command = new GetUserApplication.Command(userId);
        var response = await _mediator.Send(command, CancellationToken);

        return response switch
        {
            GetUserApplication.Success result => Ok(JsonConvert.SerializeObject(result.Application, _jsonSettings)),
            GetUserApplication.Failed result => BadRequest(result.Error),
            _ => BadRequest()
        };
    }
}