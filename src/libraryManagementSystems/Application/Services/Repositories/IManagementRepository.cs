using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IManagementRepository : IAsyncRepository<Management, Guid>, IRepository<Management, Guid>
{
}