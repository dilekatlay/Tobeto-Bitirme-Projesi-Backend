using Application.Features.InventoryManagements.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.InventoryManagements;

public class InventoryManagementManager : IInventoryManagementService
{
    private readonly IInventoryManagementRepository _inventoryManagementRepository;
    private readonly InventoryManagementBusinessRules _inventoryManagementBusinessRules;

    public InventoryManagementManager(IInventoryManagementRepository inventoryManagementRepository, InventoryManagementBusinessRules inventoryManagementBusinessRules)
    {
        _inventoryManagementRepository = inventoryManagementRepository;
        _inventoryManagementBusinessRules = inventoryManagementBusinessRules;
    }

    public async Task<InventoryManagement?> GetAsync(
        Expression<Func<InventoryManagement, bool>> predicate,
        Func<IQueryable<InventoryManagement>, IIncludableQueryable<InventoryManagement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        InventoryManagement? inventoryManagement = await _inventoryManagementRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return inventoryManagement;
    }

    public async Task<IPaginate<InventoryManagement>?> GetListAsync(
        Expression<Func<InventoryManagement, bool>>? predicate = null,
        Func<IQueryable<InventoryManagement>, IOrderedQueryable<InventoryManagement>>? orderBy = null,
        Func<IQueryable<InventoryManagement>, IIncludableQueryable<InventoryManagement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<InventoryManagement> inventoryManagementList = await _inventoryManagementRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return inventoryManagementList;
    }

    public async Task<InventoryManagement> AddAsync(InventoryManagement inventoryManagement)
    {
        InventoryManagement addedInventoryManagement = await _inventoryManagementRepository.AddAsync(inventoryManagement);

        return addedInventoryManagement;
    }

    public async Task<InventoryManagement> UpdateAsync(InventoryManagement inventoryManagement)
    {
        InventoryManagement updatedInventoryManagement = await _inventoryManagementRepository.UpdateAsync(inventoryManagement);

        return updatedInventoryManagement;
    }

    public async Task<InventoryManagement> DeleteAsync(InventoryManagement inventoryManagement, bool permanent = false)
    {
        InventoryManagement deletedInventoryManagement = await _inventoryManagementRepository.DeleteAsync(inventoryManagement);

        return deletedInventoryManagement;
    }
}
