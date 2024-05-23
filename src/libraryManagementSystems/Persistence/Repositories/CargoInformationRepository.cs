using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CargoInformationRepository : EfRepositoryBase<CargoInformation, Guid, BaseDbContext>, ICargoInformationRepository
{
    public CargoInformationRepository(BaseDbContext context) : base(context)
    {
    }
}