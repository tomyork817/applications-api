using Application.Abstractions.DataAccess;
using Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.Users.GetUserApplication;

namespace Application.Users;

public class GetUserApplicationHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _databaseContext;

    public GetUserApplicationHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var application = await _databaseContext.UnsubmittedApplications
            .Include(a => a.Activity)
            .Where(a => a.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (application is null)
        {
            return new Failed("No application for this user");
        }

        return new Success(application.AsDto());
    }
}