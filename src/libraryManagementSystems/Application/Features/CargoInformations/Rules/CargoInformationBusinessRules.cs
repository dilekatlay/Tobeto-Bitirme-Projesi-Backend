using Application.Features.CargoInformations.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.CargoInformations.Rules;

public class CargoInformationBusinessRules : BaseBusinessRules
{
    private readonly ICargoInformationRepository _cargoInformationRepository;
    private readonly ILocalizationService _localizationService;

    public CargoInformationBusinessRules(ICargoInformationRepository cargoInformationRepository, ILocalizationService localizationService)
    {
        _cargoInformationRepository = cargoInformationRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CargoInformationsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CargoInformationShouldExistWhenSelected(CargoInformation? cargoInformation)
    {
        if (cargoInformation == null)
            await throwBusinessException(CargoInformationsBusinessMessages.CargoInformationNotExists);
    }

    public async Task CargoInformationIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        CargoInformation? cargoInformation = await _cargoInformationRepository.GetAsync(
            predicate: ci => ci.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CargoInformationShouldExistWhenSelected(cargoInformation);
    }


}