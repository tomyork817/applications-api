using Application.Contracts.Activities;
using Application.Contracts.Applications;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

    [HttpGet("{applicationId:guid}")]
    public async Task<ActionResult<ApplicationDto>> GetAsync(Guid applicationId)
    {
        var command = new GetApplication.Command(applicationId);
        var response = await _mediator.Send(command, CancellationToken);

        return response switch
        {
            GetApplication.Success result => Ok(JsonConvert.SerializeObject(result.Application, _jsonSettings)),
            GetApplication.Failed result => BadRequest(result.Error),
            _ => BadRequest()
        };
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationDto>> CreateAsync([FromBody] CreateApplicationModel model)
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

    [HttpPut("{applicationId:guid}")]
    public async Task<ActionResult<ApplicationDto>> UpdateAsync(
        Guid applicationId,
        [FromBody] UpdateApplicationModel model)
    {
        var command =
            new UpdateApplication.Command(applicationId, model.Activity, model.Name, model.Description, model.Outline);
        var response = await _mediator.Send(command, CancellationToken);

        return response switch
        {
            UpdateApplication.Success result => Ok(JsonConvert.SerializeObject(result.Application, _jsonSettings)),
            UpdateApplication.Failed result => BadRequest(result.Error),
            _ => BadRequest()
        };
    }

    [HttpDelete("{applicationId:guid}")]
    public async Task<ActionResult<string>> DeleteAsync(Guid applicationId)
    {
        var command = new DeleteApplication.Command(applicationId);
        var response = await _mediator.Send(command, CancellationToken);

        return response switch
        {
            DeleteApplication.Success result => Ok(result.Message),
            DeleteApplication.Failed result => BadRequest(result.Error),
            _ => BadRequest()
        };
    }

    [HttpPost("{applicationId:guid}/submit")]
    public async Task<ActionResult<string>> SubmitAsync(Guid applicationId)
    {
        var command = new SubmitApplication.Command(applicationId);
        var response = await _mediator.Send(command, CancellationToken);

        return response switch
        {
            SubmitApplication.Success result => Ok(result.Message),
            SubmitApplication.Failed result => BadRequest(result.Error),
            _ => BadRequest()
        };
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetSubmittedApplications(
        [FromQuery] DateTime? submittedAfter, [FromQuery] DateTime? unsubmittedOlder)
    {
        if (submittedAfter is null && unsubmittedOlder is null ||
            submittedAfter is not null && unsubmittedOlder is not null)
        {
            return BadRequest();
        }

        if (submittedAfter is not null)
        {
            var commandSubmitted = new GetSubmittedApplications.Command(submittedAfter.Value.ToUniversalTime());
            var responseSubmitted = await _mediator.Send(commandSubmitted, CancellationToken);
            
            return responseSubmitted switch
            {
                GetSubmittedApplications.Success result => Ok(JsonConvert.SerializeObject(result.Applications, _jsonSettings)),
                GetSubmittedApplications.Failed result => BadRequest(result.Error),
                _ => BadRequest()
            };
        }
        
        var commandUnsubmitted = new GetUnsubmittedApplications.Command(unsubmittedOlder.Value.ToUniversalTime());
        var responseUnsubmitted = await _mediator.Send(commandUnsubmitted, CancellationToken);

        return responseUnsubmitted switch
        {
            GetUnsubmittedApplications.Success result => Ok(JsonConvert.SerializeObject(result.Applications, _jsonSettings)),
            GetUnsubmittedApplications.Failed result => BadRequest(result.Error),
            _ => BadRequest()
        };
    }
}