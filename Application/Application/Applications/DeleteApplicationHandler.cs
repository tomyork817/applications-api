using Application.Abstractions.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.Applications.DeleteApplication;

namespace Application.Applications;

public class DeleteApplicationHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _databaseContext;

    public DeleteApplicationHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var unsubmittedApplication = await _databaseContext.UnsubmittedApplications
            .Where(a => a.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (unsubmittedApplication is not null)
        {
            _databaseContext.UnsubmittedApplications.Remove(unsubmittedApplication);
            try
            {
                await _databaseContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                return new Failed("Error while removing application from database");
            }

            return new Success("Ok");
        }

        var submittedApplication = await _databaseContext.SubmittedApplications
            .Where(a => a.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (submittedApplication is null)
        {
            return new Failed("No application with such id");
        }
        
        _databaseContext.SubmittedApplications.Remove(submittedApplication);
        try
        {
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            return new Failed("Error while removing application from database");
        }

        return new Success("Ok");
    }
}