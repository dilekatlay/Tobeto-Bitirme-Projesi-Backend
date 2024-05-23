using Application.Features.CargoTrackings.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.CargoTrackings.Rules;

public class CargoTrackingBusinessRules : BaseBusinessRules
{
    private readonly ICargoTrackingRepository _cargoTrackingRepository;
    private readonly ILocalizationService _localizationService;

    public CargoTrackingBusinessRules(ICargoTrackingRepository cargoTrackingRepository, ILocalizationService localizationService)
    {
        _cargoTrackingRepository = cargoTrackingRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CargoTrackingsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CargoTrackingShouldExistWhenSelected(CargoTracking? cargoTracking)
    {
        if (cargoTracking == null)
            await throwBusinessException(CargoTrackingsBusinessMessages.CargoTrackingNotExists);
    }

    public async Task CargoTrackingIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        CargoTracking? cargoTracking = await _cargoTrackingRepository.GetAsync(
            predicate: ct => ct.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CargoTrackingShouldExistWhenSelected(cargoTracking);
    }
}