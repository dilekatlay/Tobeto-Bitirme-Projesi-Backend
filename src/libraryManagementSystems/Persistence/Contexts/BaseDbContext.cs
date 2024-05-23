using System.Reflection;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<LoanTransaction> LoanTransactions { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<CargoInformation> CargoInformations { get; set; }
    public DbSet<CargoTracking> CargoTrackings { get; set; }
    public DbSet<CreditCart> CreditCarts { get; set; }
    public DbSet<Management> Managements { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<InventoryManagement> InventoryManagements { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Shelf> Shelves { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Member> Members { get; set; }


    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Order>().Property(o => o.OrderStatu).HasConversion<string>();
        modelBuilder.Entity<Order>().Property(b => b.PaymentMethod).HasConversion<string>();



    }
}
