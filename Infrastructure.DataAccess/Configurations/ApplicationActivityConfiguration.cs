using Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

public class ApplicationActivityConfiguration : IEntityTypeConfiguration<ApplicationActivity>
{
    public void Configure(EntityTypeBuilder<ApplicationActivity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Description);
    }
}