using Application.Features.CargoInformations.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CargoInformations;

public class CargoInformationManager : ICargoInformationService
{
    private readonly ICargoInformationRepository _cargoInformationRepository;
    private readonly CargoInformationBusinessRules _cargoInformationBusinessRules;

    public CargoInformationManager(ICargoInformationRepository cargoInformationRepository, CargoInformationBusinessRules cargoInformationBusinessRules)
    {
        _cargoInformationRepository = cargoInformationRepository;
        _cargoInformationBusinessRules = cargoInformationBusinessRules;
    }

    public async Task<CargoInformation?> GetAsync(
        Expression<Func<CargoInformation, bool>> predicate,
        Func<IQueryable<CargoInformation>, IIncludableQueryable<CargoInformation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        CargoInformation? cargoInformation = await _cargoInformationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return cargoInformation;
    }

    public async Task<IPaginate<CargoInformation>?> GetListAsync(
        Expression<Func<CargoInformation, bool>>? predicate = null,
        Func<IQueryable<CargoInformation>, IOrderedQueryable<CargoInformation>>? orderBy = null,
        Func<IQueryable<CargoInformation>, IIncludableQueryable<CargoInformation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<CargoInformation> cargoInformationList = await _cargoInformationRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return cargoInformationList;
    }

    public async Task<CargoInformation> AddAsync(CargoInformation cargoInformation)
    {
        CargoInformation addedCargoInformation = await _cargoInformationRepository.AddAsync(cargoInformation);

        return addedCargoInformation;
    }

    public async Task<CargoInformation> UpdateAsync(CargoInformation cargoInformation)
    {
        CargoInformation updatedCargoInformation = await _cargoInformationRepository.UpdateAsync(cargoInformation);

        return updatedCargoInformation;
    }

    public async Task<CargoInformation> DeleteAsync(CargoInformation cargoInformation, bool permanent = false)
    {
        CargoInformation deletedCargoInformation = await _cargoInformationRepository.DeleteAsync(cargoInformation);

        return deletedCargoInformation;
    }
}
