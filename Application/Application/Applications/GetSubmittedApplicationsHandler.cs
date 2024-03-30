using Application.Abstractions.DataAccess;
using Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.Applications.GetSubmittedApplications;

namespace Application.Applications;

public class GetSubmittedApplicationsHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _databaseContext;

    public GetSubmittedApplicationsHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var applications = await _databaseContext.SubmittedApplications
            .Include(a => a.Activity)
            .Where(a => a.CreatedDateTime > request.Time)
            .ToListAsync(cancellationToken);

        return new Success(applications.Select(a => a.AsDto()));
    }
}