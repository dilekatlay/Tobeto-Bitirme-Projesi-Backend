using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Shelves;

public interface IShelfService
{
    Task<Shelf?> GetAsync(
        Expression<Func<Shelf, bool>> predicate,
        Func<IQueryable<Shelf>, IIncludableQueryable<Shelf, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Shelf>?> GetListAsync(
        Expression<Func<Shelf, bool>>? predicate = null,
        Func<IQueryable<Shelf>, IOrderedQueryable<Shelf>>? orderBy = null,
        Func<IQueryable<Shelf>, IIncludableQueryable<Shelf, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Shelf> AddAsync(Shelf shelf);
    Task<Shelf> UpdateAsync(Shelf shelf);
    Task<Shelf> DeleteAsync(Shelf shelf, bool permanent = false);
}
