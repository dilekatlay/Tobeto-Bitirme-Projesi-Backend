using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CargoTrackings;

public interface ICargoTrackingService
{
    Task<CargoTracking?> GetAsync(
        Expression<Func<CargoTracking, bool>> predicate,
        Func<IQueryable<CargoTracking>, IIncludableQueryable<CargoTracking, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<CargoTracking>?> GetListAsync(
        Expression<Func<CargoTracking, bool>>? predicate = null,
        Func<IQueryable<CargoTracking>, IOrderedQueryable<CargoTracking>>? orderBy = null,
        Func<IQueryable<CargoTracking>, IIncludableQueryable<CargoTracking, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<CargoTracking> AddAsync(CargoTracking cargoTracking);
    Task<CargoTracking> UpdateAsync(CargoTracking cargoTracking);
    Task<CargoTracking> DeleteAsync(CargoTracking cargoTracking, bool permanent = false);
}
