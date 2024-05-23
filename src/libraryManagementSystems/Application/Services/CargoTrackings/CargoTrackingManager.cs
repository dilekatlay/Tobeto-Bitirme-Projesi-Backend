using Application.Features.CargoTrackings.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CargoTrackings;

public class CargoTrackingManager : ICargoTrackingService
{
    private readonly ICargoTrackingRepository _cargoTrackingRepository;
    private readonly CargoTrackingBusinessRules _cargoTrackingBusinessRules;

    public CargoTrackingManager(ICargoTrackingRepository cargoTrackingRepository, CargoTrackingBusinessRules cargoTrackingBusinessRules)
    {
        _cargoTrackingRepository = cargoTrackingRepository;
        _cargoTrackingBusinessRules = cargoTrackingBusinessRules;
    }

    public async Task<CargoTracking?> GetAsync(
        Expression<Func<CargoTracking, bool>> predicate,
        Func<IQueryable<CargoTracking>, IIncludableQueryable<CargoTracking, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        CargoTracking? cargoTracking = await _cargoTrackingRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return cargoTracking;
    }

    public async Task<IPaginate<CargoTracking>?> GetListAsync(
        Expression<Func<CargoTracking, bool>>? predicate = null,
        Func<IQueryable<CargoTracking>, IOrderedQueryable<CargoTracking>>? orderBy = null,
        Func<IQueryable<CargoTracking>, IIncludableQueryable<CargoTracking, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<CargoTracking> cargoTrackingList = await _cargoTrackingRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return cargoTrackingList;
    }

    public async Task<CargoTracking> AddAsync(CargoTracking cargoTracking)
    {
        CargoTracking addedCargoTracking = await _cargoTrackingRepository.AddAsync(cargoTracking);

        return addedCargoTracking;
    }

    public async Task<CargoTracking> UpdateAsync(CargoTracking cargoTracking)
    {
        CargoTracking updatedCargoTracking = await _cargoTrackingRepository.UpdateAsync(cargoTracking);

        return updatedCargoTracking;
    }

    public async Task<CargoTracking> DeleteAsync(CargoTracking cargoTracking, bool permanent = false)
    {
        CargoTracking deletedCargoTracking = await _cargoTrackingRepository.DeleteAsync(cargoTracking);

        return deletedCargoTracking;
    }
}
