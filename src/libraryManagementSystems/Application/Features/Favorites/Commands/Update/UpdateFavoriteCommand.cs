using Application.Features.Favorites.Constants;
using Application.Features.Favorites.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.Features.Favorites.Constants.FavoritesOperationClaims;

namespace Application.Features.Favorites.Commands.Update;

public class UpdateFavoriteCommand : IRequest<UpdatedFavoriteResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }

    public string[] Roles => [Admin, Write, FavoritesOperationClaims.Update];

    public class UpdateFavoriteCommandHandler : IRequestHandler<UpdateFavoriteCommand, UpdatedFavoriteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly FavoriteBusinessRules _favoriteBusinessRules;

        public UpdateFavoriteCommandHandler(IMapper mapper, IFavoriteRepository favoriteRepository,
                                         FavoriteBusinessRules favoriteBusinessRules)
        {
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _favoriteBusinessRules = favoriteBusinessRules;
        }

        public async Task<UpdatedFavoriteResponse> Handle(UpdateFavoriteCommand request, CancellationToken cancellationToken)
        {
            Favorite? favorite = await _favoriteRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _favoriteBusinessRules.FavoriteShouldExistWhenSelected(favorite);
            favorite = _mapper.Map(request, favorite);

            await _favoriteRepository.UpdateAsync(favorite!);

            UpdatedFavoriteResponse response = _mapper.Map<UpdatedFavoriteResponse>(favorite);
            return response;
        }
    }
}