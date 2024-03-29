using Application.Abstractions.DataAccess;
using Application.Dto;
using Application.Mapping;
using Domain.Applications;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using static Application.Contracts.Applications.CreateApplication;


namespace Application.Applications;

public class CreateApplicationHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _databaseContext;
    private readonly IValidator<Command> _validator;

    public CreateApplicationHandler(IDatabaseContext databaseContext, IValidator<Command> validator)
    {
        _databaseContext = databaseContext;
        _validator = validator;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Failed(Strings.Join(validationResult.Errors.Select(e => e.ErrorMessage).ToArray(), "\n"));
        }

        var application = new UnsubmittedApplication(
            Guid.NewGuid(),
            request.Author ?? throw new ArgumentNullException(nameof(request)),
            null,
            request.Name,
            request.Description,
            request.Outline,
            DateTime.Now.ToUniversalTime());

        if (request.Activity is not null)
        {
            var activity =
                await _databaseContext.ApplicationActivities.FirstOrDefaultAsync(x => x.Name == request.Activity,
                    cancellationToken);
            application.Activity = activity;
        }

        _databaseContext.UnsubmittedApplications.Add(application);

        try
        {
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            return new Failed("Error while adding to database");
        }
        
        return new Success(application.AsDto());
    }
}