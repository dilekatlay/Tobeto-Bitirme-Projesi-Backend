using Application.Features.Favorites.Constants;
using Application.Features.Favorites.Constants;
using Application.Features.Favorites.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.Features.Favorites.Constants.FavoritesOperationClaims;

namespace Application.Features.Favorites.Commands.Delete;

public class DeleteFavoriteCommand : IRequest<DeletedFavoriteResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, FavoritesOperationClaims.Delete];

    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, DeletedFavoriteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly FavoriteBusinessRules _favoriteBusinessRules;

        public DeleteFavoriteCommandHandler(IMapper mapper, IFavoriteRepository favoriteRepository,
                                         FavoriteBusinessRules favoriteBusinessRules)
        {
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _favoriteBusinessRules = favoriteBusinessRules;
        }

        public async Task<DeletedFavoriteResponse> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            Favorite? favorite = await _favoriteRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _favoriteBusinessRules.FavoriteShouldExistWhenSelected(favorite);

            await _favoriteRepository.DeleteAsync(favorite!);

            DeletedFavoriteResponse response = _mapper.Map<DeletedFavoriteResponse>(favorite);
            return response;
        }
    }
}