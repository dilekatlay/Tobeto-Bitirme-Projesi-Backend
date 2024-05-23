using Application.Services.Repositories;
using Application.Services.TranslateService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Persistence.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BaseDb")));
        services.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<BaseDbContext>());

        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        services.AddScoped<ILoanTransactionRepository, LoanTransactionRepository>();

        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICargoInformationRepository, CargoInformationRepository>();
        services.AddScoped<ICargoTrackingRepository, CargoTrackingRepository>();
        services.AddScoped<ICreditCartRepository, CreditCartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IManagementRepository, ManagementRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IInventoryManagementRepository, InventoryManagementRepository>();
        services.AddScoped<ICargoInformationRepository, CargoInformationRepository>();
        services.AddScoped<ICargoTrackingRepository, CargoTrackingRepository>();
        services.AddScoped<ICreditCartRepository, CreditCartRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IInventoryManagementRepository, InventoryManagementRepository>();
        services.AddScoped<ILoanTransactionRepository, LoanTransactionRepository>();
        services.AddScoped<IManagementRepository, ManagementRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IShelfRepository, ShelfRepository>();
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();


        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }
}
