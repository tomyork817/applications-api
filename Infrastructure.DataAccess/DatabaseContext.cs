using Application.Abstractions.DataAccess;
using Domain.Applications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<ApplicationActivity> ApplicationActivities { get; set; }
    public DbSet<UnsubmittedApplication> UnsubmittedApplications { get; set; }
    public DbSet<SubmittedApplication> SubmittedApplications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}