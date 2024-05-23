using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CargoInformations;

public interface ICargoInformationService
{
    Task<CargoInformation?> GetAsync(
        Expression<Func<CargoInformation, bool>> predicate,
        Func<IQueryable<CargoInformation>, IIncludableQueryable<CargoInformation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<CargoInformation>?> GetListAsync(
        Expression<Func<CargoInformation, bool>>? predicate = null,
        Func<IQueryable<CargoInformation>, IOrderedQueryable<CargoInformation>>? orderBy = null,
        Func<IQueryable<CargoInformation>, IIncludableQueryable<CargoInformation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<CargoInformation> AddAsync(CargoInformation cargoInformation);
    Task<CargoInformation> UpdateAsync(CargoInformation cargoInformation);
    Task<CargoInformation> DeleteAsync(CargoInformation cargoInformation, bool permanent = false);
}
