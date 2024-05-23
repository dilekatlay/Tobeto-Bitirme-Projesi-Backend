using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.BookId).HasColumnName("BookId");
        builder.Property(r => r.MemberID).HasColumnName("MemberID");
        builder.Property(r => r.ReservationDate).HasColumnName("ReservationDate");
        builder.Property(r => r.ReservationEndDate).HasColumnName("ReservationEndDate");
        builder.Property(r => r.IsReserv).HasColumnName("IsReserv");
        builder.Property(r => r.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(r => r.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(r => r.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(r => !r.DeletedDate.HasValue);

        builder.HasOne(r => r.Book)
            .WithMany()
            .HasForeignKey(r => r.BookId).OnDelete(DeleteBehavior.Restrict);

    }
}