using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CargoTrackingRepository : EfRepositoryBase<CargoTracking, Guid, BaseDbContext>, ICargoTrackingRepository
{
    public CargoTrackingRepository(BaseDbContext context) : base(context)
    {
    }
}