using Domain.Applications;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.DataAccess;

public interface IDatabaseContext
{
    DbSet<ApplicationActivity> ApplicationActivities { get; set; }
    DbSet<UnsubmittedApplication> UnsubmittedApplications { get; set; }
    DbSet<SubmittedApplication> SubmittedApplications { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}