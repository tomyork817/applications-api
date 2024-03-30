using Application.Abstractions.DataAccess;
using Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Applications;
using static Application.Contracts.Applications.GetApplication;

public class GetApplicationHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _databaseContext;

    public GetApplicationHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var unsubmittedApplication = await _databaseContext.UnsubmittedApplications
            .Include(a => a.Activity)
            .Where(a => a.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (unsubmittedApplication is not null)
        {
            return new Success(unsubmittedApplication.AsDto());
        }
        
        var submittedApplication = await _databaseContext.SubmittedApplications
            .Include(a => a.Activity)
            .Where(a => a.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (submittedApplication is not null)
        {
            return new Success(submittedApplication.AsDto());
        }

        return new Failed("No application with such id");
    }
}