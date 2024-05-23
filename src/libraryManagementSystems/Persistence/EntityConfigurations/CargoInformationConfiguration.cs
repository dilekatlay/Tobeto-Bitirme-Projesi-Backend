using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CargoInformationConfiguration : IEntityTypeConfiguration<CargoInformation>
{
    public void Configure(EntityTypeBuilder<CargoInformation> builder)
    {
        builder.ToTable("CargoInformations").HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id).HasColumnName("Id").IsRequired();
        builder.Property(ci => ci.CompanyName).HasColumnName("CompanyName");
        builder.Property(ci => ci.Adress).HasColumnName("Adress");
        builder.Property(ci => ci.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(ci => ci.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ci => ci.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ci => ci.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ci => !ci.DeletedDate.HasValue);
    }
}