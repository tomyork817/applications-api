using Application.Abstractions.DataAccess;
using Application.Mapping;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using static Application.Contracts.Applications.UpdateApplication;

namespace Application.Applications;

public class UpdateApplicationHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _databaseContext;
    private readonly IValidator<Command> _validator;

    public UpdateApplicationHandler(IDatabaseContext databaseContext, IValidator<Command> validator)
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
        
        var application = await _databaseContext.UnsubmittedApplications
            .Where(a => a.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (application == null)
        {
            return new Failed("Application not found");
        }
        
        Console.WriteLine(request.Activity);
        
        if (request.Activity is not null)
        {
            var activity =
                await _databaseContext.ApplicationActivities.FirstOrDefaultAsync(x => x.Name == request.Activity,
                    cancellationToken);
            application.Activity = activity;
        }
        else
        {
            application.Activity = null;
        }
        
        application.Name = request.Name;
        application.Description = request.Description;
        application.Plan = request.Outline;
        
        try
        {
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            return new Failed("Error while changing application in   database");
        }
        
        return new Success(application.AsDto());
    }
}