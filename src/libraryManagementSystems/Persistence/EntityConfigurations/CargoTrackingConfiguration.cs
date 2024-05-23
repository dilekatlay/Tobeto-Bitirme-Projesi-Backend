using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CargoTrackingConfiguration : IEntityTypeConfiguration<CargoTracking>
{
    public void Configure(EntityTypeBuilder<CargoTracking> builder)
    {
        builder.ToTable("CargoTrackings").HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id).HasColumnName("Id").IsRequired();
        builder.Property(ct => ct.CargoTrackingNo).HasColumnName("CargoTrackingNo");
        builder.Property(ct => ct.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ct => ct.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ct => ct.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ct => !ct.DeletedDate.HasValue);
    }
}