using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees").HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
        builder.Property(e => e.UserId).HasColumnName("UserId");
        builder.Property(e => e.UserName).HasColumnName("UserName");
        builder.Property(e => e.Password).HasColumnName("Password");
        builder.Property(e => e.FirstName).HasColumnName("FirstName");
        builder.Property(e => e.LastName).HasColumnName("LastName");
        builder.Property(e => e.Title).HasColumnName("Title");
        builder.Property(e => e.Contact).HasColumnName("Contact");
        builder.Property(e => e.WorkingHours).HasColumnName("WorkingHours");
        builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(e => e.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(e => e.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(e => !e.DeletedDate.HasValue);

        builder.HasOne(e => e.User)
       .WithMany()
       .HasForeignKey(e => e.UserId)
       .OnDelete(DeleteBehavior.Restrict);
    }
}