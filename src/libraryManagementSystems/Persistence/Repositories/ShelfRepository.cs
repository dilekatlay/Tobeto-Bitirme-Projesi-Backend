using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ShelfRepository : EfRepositoryBase<Shelf, Guid, BaseDbContext>, IShelfRepository
{
    public ShelfRepository(BaseDbContext context) : base(context)
    {
    }
}