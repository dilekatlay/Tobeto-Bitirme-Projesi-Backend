using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IInventoryManagementRepository : IAsyncRepository<InventoryManagement, Guid>, IRepository<InventoryManagement, Guid>
{
}