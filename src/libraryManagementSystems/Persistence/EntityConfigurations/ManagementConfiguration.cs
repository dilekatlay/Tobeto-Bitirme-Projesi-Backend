using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ManagementConfiguration : IEntityTypeConfiguration<Management>
{
    public void Configure(EntityTypeBuilder<Management> builder)
    {
        builder.ToTable("Managements").HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
        builder.Property(m => m.InventoryId).HasColumnName("InventoryId");
        builder.Property(m => m.BookId).HasColumnName("BookId");
        builder.Property(m => m.UserId).HasColumnName("UserId");
        builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(m => m.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(m => !m.DeletedDate.HasValue);

        builder.HasOne(m => m.InventoryManagement)
            .WithMany()
            .HasForeignKey(m => m.InventoryId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Book)
            .WithMany()
            .HasForeignKey(m => m.BookId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);





    }
}