using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CreditCartConfiguration : IEntityTypeConfiguration<CreditCart>
{
    public void Configure(EntityTypeBuilder<CreditCart> builder)
    {
        builder.ToTable("CreditCarts").HasKey(cc => cc.Id);

        builder.Property(cc => cc.Id).HasColumnName("Id").IsRequired();
        builder.Property(cc => cc.NameOnTheCart).HasColumnName("NameOnTheCart");
        builder.Property(cc => cc.CartNo).HasColumnName("CartNo");
        builder.Property(cc => cc.ExpirationDate).HasColumnName("ExpirationDate");
        builder.Property(cc => cc.Cvv).HasColumnName("Cvv");
        builder.Property(cc => cc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cc => cc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cc => cc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cc => !cc.DeletedDate.HasValue);
    }
}