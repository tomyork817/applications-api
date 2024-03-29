using Application.Contracts.Activities;
using Application.Contracts.Applications;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Models.Applications;

namespace Presentation.Controllers;

[ApiController]
[Route("/applications")]
public class ApplicationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly JsonSerializerSettings _jsonSettings;

    public ApplicationsController(IMediator mediator, JsonSerializerSettings jsonSettings)
    {
        _mediator = mediator;
        _jsonSettings = jsonSettings;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<UnsubmittedApplicationDto>> CreateAsync([FromBody] CreateApplicationModel model)
    {
        var command =
            new CreateApplication.Command(model.Author, model.Activity, model.Name, model.Description, model.Outline);
        var response = await _mediator.Send(command, CancellationToken);

        return response switch
        {
            CreateApplication.Success result => Ok(JsonConvert.SerializeObject(result.Application, _jsonSettings)),
            CreateApplication.Failed result => BadRequest(result.Error),
            _ => BadRequest()
        };
    }
}