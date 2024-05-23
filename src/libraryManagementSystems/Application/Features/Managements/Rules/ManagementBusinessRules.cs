using Application.Features.Managements.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Managements.Rules;

public class ManagementBusinessRules : BaseBusinessRules
{
    private readonly IManagementRepository _managementRepository;
    private readonly ILocalizationService _localizationService;

    public ManagementBusinessRules(IManagementRepository managementRepository, ILocalizationService localizationService)
    {
        _managementRepository = managementRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ManagementsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ManagementShouldExistWhenSelected(Management? management)
    {
        if (management == null)
            await throwBusinessException(ManagementsBusinessMessages.ManagementNotExists);
    }

    public async Task ManagementIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Management? management = await _managementRepository.GetAsync(
            predicate: m => m.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ManagementShouldExistWhenSelected(management);
    }
}