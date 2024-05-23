using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.InventoryManagements;

public interface IInventoryManagementService
{
    Task<InventoryManagement?> GetAsync(
        Expression<Func<InventoryManagement, bool>> predicate,
        Func<IQueryable<InventoryManagement>, IIncludableQueryable<InventoryManagement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<InventoryManagement>?> GetListAsync(
        Expression<Func<InventoryManagement, bool>>? predicate = null,
        Func<IQueryable<InventoryManagement>, IOrderedQueryable<InventoryManagement>>? orderBy = null,
        Func<IQueryable<InventoryManagement>, IIncludableQueryable<InventoryManagement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<InventoryManagement> AddAsync(InventoryManagement inventoryManagement);
    Task<InventoryManagement> UpdateAsync(InventoryManagement inventoryManagement);
    Task<InventoryManagement> DeleteAsync(InventoryManagement inventoryManagement, bool permanent = false);
}
