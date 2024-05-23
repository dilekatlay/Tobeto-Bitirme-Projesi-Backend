using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICargoTrackingRepository : IAsyncRepository<CargoTracking, Guid>, IRepository<CargoTracking, Guid>
{
}