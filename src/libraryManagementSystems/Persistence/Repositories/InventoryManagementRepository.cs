using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class InventoryManagementRepository : EfRepositoryBase<InventoryManagement, Guid, BaseDbContext>, IInventoryManagementRepository
{
    public InventoryManagementRepository(BaseDbContext context) : base(context)
    {
    }
}