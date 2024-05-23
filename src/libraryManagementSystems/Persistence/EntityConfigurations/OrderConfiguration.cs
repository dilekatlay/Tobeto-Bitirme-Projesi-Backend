using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders").HasKey(o => o.Id);

        builder.Property(o => o.Id).HasColumnName("Id").IsRequired();
        builder.Property(o => o.BookId).HasColumnName("BookId");
        builder.Property(o => o.MemberId).HasColumnName("MemberId");
        builder.Property(o => o.UnitPrice).HasColumnName("UnitPrice");
        builder.Property(o => o.OrderDate).HasColumnName("OrderDate");
        builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(o => o.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(o => o.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(o => !o.DeletedDate.HasValue);

        builder.HasOne(o => o.Book)
            .WithMany()
            .HasForeignKey(o => o.BookId).OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(o => o.Member);

    }
}