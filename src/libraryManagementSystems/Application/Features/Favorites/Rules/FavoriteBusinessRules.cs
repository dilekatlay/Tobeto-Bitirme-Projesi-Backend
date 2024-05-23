using Application.Features.Favorites.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Favorites.Rules;

public class FavoriteBusinessRules : BaseBusinessRules
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly ILocalizationService _localizationService;

    public FavoriteBusinessRules(IFavoriteRepository favoriteRepository, ILocalizationService localizationService)
    {
        _favoriteRepository = favoriteRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, FavoritesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task FavoriteShouldExistWhenSelected(Favorite? favorite)
    {
        if (favorite == null)
            await throwBusinessException(FavoritesBusinessMessages.FavoriteNotExists);
    }

    public async Task FavoriteIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Favorite? favorite = await _favoriteRepository.GetAsync(
            predicate: f => f.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FavoriteShouldExistWhenSelected(favorite);
    }
}