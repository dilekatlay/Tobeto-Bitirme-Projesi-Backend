using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books").HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.ISBNNo).HasColumnName("ISBNNo");
        builder.Property(b => b.BookName).HasColumnName("BookName");
        builder.Property(b => b.Summary).HasColumnName("Summary");
        builder.Property(b => b.Writer).HasColumnName("Writer");
        builder.Property(b => b.imageUrl).HasColumnName("ImageUrl");
        builder.Property(b => b.NumberOfCopies).HasColumnName("NumberOfCopies");
        builder.Property(b => b.NumberOfPages).HasColumnName("NumberOfPages");
        builder.Property(b => b.UnitPrice).HasColumnName("UnitPrice");
        builder.Property(b => b.CategoryId).HasColumnName("CategoryId");
        builder.Property(b => b.ShelfId).HasColumnName("ShelfId");
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);



        builder.HasOne(b => b.Category)
       .WithMany()
       .HasForeignKey(b => b.CategoryId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Shelf)
               .WithMany()
               .HasForeignKey(b => b.ShelfId)
               .OnDelete(DeleteBehavior.Restrict);


    }
}