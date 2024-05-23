using Application.Features.Shelves.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using System.Threading;
using Nest;

namespace Application.Features.Shelves.Rules;

public class ShelfBusinessRules : BaseBusinessRules
{
    private readonly IShelfRepository _shelfRepository;
    private readonly ILocalizationService _localizationService;

    public ShelfBusinessRules(IShelfRepository shelfRepository, ILocalizationService localizationService)
    {
        _shelfRepository = shelfRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ShelvesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ShelfShouldExistWhenSelected(Shelf? shelf)
    {
        if (shelf == null)
            await throwBusinessException(ShelvesBusinessMessages.ShelfNotExists);
    }

    public async Task ShelfIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Shelf? shelf = await _shelfRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ShelfShouldExistWhenSelected(shelf);
    }

    public async void DecreaseShelfCapacityForAddedBook(Guid id)
    {
        // Veritabanýndan ilgili rafý al
        var shelf = await _shelfRepository.GetAsync(predicate: s => s.Id == id);

        // Raf varsa ve kapasite 0'dan büyükse
        if (shelf != null && shelf.Capacity > 0)
        {
            // Kapasiteyi 1 azalt
            shelf.Capacity--;

            // Rafý güncelle
            _shelfRepository.Update(shelf);
        }
        else
        {
            // Hata durumunda uygun iþlem yapýlabilir, örneðin loglama yapýlabilir veya hata fýrlatýlabilir.
            throw new Exception("Raf bulunamadý veya kapasite sýfýr veya daha az.");
        }
    }

}