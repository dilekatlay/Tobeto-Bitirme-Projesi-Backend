using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ManagementRepository : EfRepositoryBase<Management, Guid, BaseDbContext>, IManagementRepository
{
    public ManagementRepository(BaseDbContext context) : base(context)
    {
    }
}