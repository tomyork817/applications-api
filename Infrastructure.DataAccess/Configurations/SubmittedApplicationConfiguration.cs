using Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

public class SubmittedApplicationConfiguration : IEntityTypeConfiguration<SubmittedApplication>
{
    public void Configure(EntityTypeBuilder<SubmittedApplication> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.UserId);
        
        builder.Property(x => x.Name);
        builder.Property(x => x.Description);
        builder.Property(x => x.Plan);
        builder.Property(x => x.CreatedDateTime);
        
        builder.HasOne(x => x.Activity)
            .WithMany();
    }
}