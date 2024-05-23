using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IShelfRepository : IAsyncRepository<Shelf, Guid>, IRepository<Shelf, Guid>
{
}