using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CreditCartRepository : EfRepositoryBase<CreditCart, Guid, BaseDbContext>, ICreditCartRepository
{
    public CreditCartRepository(BaseDbContext context) : base(context)
    {
    }
}