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
        
        if (unsubmittedApplication is null)
        {
            return new Failed("Application not found");
        }
        
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
}