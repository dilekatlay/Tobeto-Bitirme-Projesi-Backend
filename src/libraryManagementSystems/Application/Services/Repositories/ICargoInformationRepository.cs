using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICargoInformationRepository : IAsyncRepository<CargoInformation, Guid>, IRepository<CargoInformation, Guid>
{
}