using Application.Features.InventoryManagements.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.InventoryManagements.Rules;

public class InventoryManagementBusinessRules : BaseBusinessRules
{
    private readonly IInventoryManagementRepository _inventoryManagementRepository;
    private readonly ILocalizationService _localizationService;

    public InventoryManagementBusinessRules(IInventoryManagementRepository inventoryManagementRepository, ILocalizationService localizationService)
    {
        _inventoryManagementRepository = inventoryManagementRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, InventoryManagementsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task InventoryManagementShouldExistWhenSelected(InventoryManagement? inventoryManagement)
    {
        if (inventoryManagement == null)
            await throwBusinessException(InventoryManagementsBusinessMessages.InventoryManagementNotExists);
    }

    public async Task InventoryManagementIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        InventoryManagement? inventoryManagement = await _inventoryManagementRepository.GetAsync(
            predicate: im => im.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await InventoryManagementShouldExistWhenSelected(inventoryManagement);
    }
}