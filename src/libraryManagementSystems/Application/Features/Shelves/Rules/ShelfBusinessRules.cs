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
        // Veritaban�ndan ilgili raf� al
        var shelf = await _shelfRepository.GetAsync(predicate: s => s.Id == id);

        // Raf varsa ve kapasite 0'dan b�y�kse
        if (shelf != null && shelf.Capacity > 0)
        {
            // Kapasiteyi 1 azalt
            shelf.Capacity--;

            // Raf� g�ncelle
            _shelfRepository.Update(shelf);
        }
        else
        {
            // Hata durumunda uygun i�lem yap�labilir, �rne�in loglama yap�labilir veya hata f�rlat�labilir.
            throw new Exception("Raf bulunamad� veya kapasite s�f�r veya daha az.");
        }
    }

}