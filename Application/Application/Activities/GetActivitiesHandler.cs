using Application.Abstractions.DataAccess;
using Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.Activities.GetActivities;

namespace Application.Activities;

public class GetActivitiesHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _databaseContext;

    public GetActivitiesHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var activities = await _databaseContext.ApplicationActivities.ToListAsync(cancellationToken);

        return new Response(activities.Select(x => x.AsDto()));
    }
}