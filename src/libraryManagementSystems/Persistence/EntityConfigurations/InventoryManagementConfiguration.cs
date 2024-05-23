using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class InventoryManagementConfiguration : IEntityTypeConfiguration<InventoryManagement>
{
    public void Configure(EntityTypeBuilder<InventoryManagement> builder)
    {
        builder.ToTable("InventoryManagements").HasKey(im => im.Id);

        builder.Property(im => im.Id).HasColumnName("Id").IsRequired();
        builder.Property(im => im.BookId).HasColumnName("BookId");
        builder.Property(im => im.ShelfId).HasColumnName("ShelfId");
        builder.Property(im => im.CategoryId).HasColumnName("CategoryId");
        builder.Property(im => im.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(im => im.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(im => im.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(im => !im.DeletedDate.HasValue);

        builder.HasOne(im => im.Book)
       .WithMany()
       .HasForeignKey(im => im.BookId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(im => im.Shelf)
               .WithMany()
               .HasForeignKey(im => im.ShelfId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(im => im.Category)
               .WithMany()
               .HasForeignKey(im => im.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);


    }
}