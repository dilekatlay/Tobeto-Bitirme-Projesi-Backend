using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Managements;

public interface IManagementService
{
    Task<Management?> GetAsync(
        Expression<Func<Management, bool>> predicate,
        Func<IQueryable<Management>, IIncludableQueryable<Management, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Management>?> GetListAsync(
        Expression<Func<Management, bool>>? predicate = null,
        Func<IQueryable<Management>, IOrderedQueryable<Management>>? orderBy = null,
        Func<IQueryable<Management>, IIncludableQueryable<Management, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Management> AddAsync(Management management);
    Task<Management> UpdateAsync(Management management);
    Task<Management> DeleteAsync(Management management, bool permanent = false);
}
