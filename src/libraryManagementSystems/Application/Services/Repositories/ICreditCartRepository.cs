using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICreditCartRepository : IAsyncRepository<CreditCart, Guid>, IRepository<CreditCart, Guid>
{
}