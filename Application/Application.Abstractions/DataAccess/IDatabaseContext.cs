using Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Abstractions.DataAccess;

public interface IDatabaseContext
{
    DbSet<ApplicationActivity> ApplicationActivities { get; set; }
    DbSet<UnsubmittedApplication> UnsubmittedApplications { get; set; }
    DbSet<SubmittedApplication> SubmittedApplications { get; set; }
    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}