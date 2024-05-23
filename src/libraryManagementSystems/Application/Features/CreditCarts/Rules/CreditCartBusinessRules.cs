using Application.Features.CreditCarts.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.CreditCarts.Rules;

public class CreditCartBusinessRules : BaseBusinessRules
{
    private readonly ICreditCartRepository _creditCartRepository;
    private readonly ILocalizationService _localizationService;

    public CreditCartBusinessRules(ICreditCartRepository creditCartRepository, ILocalizationService localizationService)
    {
        _creditCartRepository = creditCartRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CreditCartsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CreditCartShouldExistWhenSelected(CreditCart? creditCart)
    {
        if (creditCart == null)
            await throwBusinessException(CreditCartsBusinessMessages.CreditCartNotExists);
    }

    public async Task CreditCartIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        CreditCart? creditCart = await _creditCartRepository.GetAsync(
            predicate: cc => cc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CreditCartShouldExistWhenSelected(creditCart);
    }

    public async Task<bool> CartNoValid(string cartNo)
    {
        // Kart numaras�n�n uzunlu�unu asenkron olarak kontrol et
        await Task.Delay(0); // Asenkron olmas�n� sa�lamak i�in await kullan�ld�

        // Kart numaras�n�n 16 karakter olmas� gerekti�ini kontrol et
        if (cartNo.Length != 16)
        {
            return false;
        }

        // Kart numaras�n�n sadece rakamlardan olu�tu�unu kontrol et
        foreach (char karakter in cartNo)
        {
            if (!char.IsDigit(karakter))
            {
                return false;
            }
        }

        // Ge�erli ise true d�nd�r
        return true;
    }


}

