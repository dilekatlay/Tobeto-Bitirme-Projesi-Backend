using Application.Features.Favorites.Constants;
using Application.Features.Favorites.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Favorites.Constants.FavoritesOperationClaims;

namespace Application.Features.Favorites.Queries.GetById;

public class GetByIdFavoriteQuery : IRequest<GetByIdFavoriteResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdFavoriteQueryHandler : IRequestHandler<GetByIdFavoriteQuery, GetByIdFavoriteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly FavoriteBusinessRules _favoriteBusinessRules;

        public GetByIdFavoriteQueryHandler(IMapper mapper, IFavoriteRepository favoriteRepository, FavoriteBusinessRules favoriteBusinessRules)
        {
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _favoriteBusinessRules = favoriteBusinessRules;
        }

        public async Task<GetByIdFavoriteResponse> Handle(GetByIdFavoriteQuery request, CancellationToken cancellationToken)
        {
            Favorite? favorite = await _favoriteRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _favoriteBusinessRules.FavoriteShouldExistWhenSelected(favorite);

            GetByIdFavoriteResponse response = _mapper.Map<GetByIdFavoriteResponse>(favorite);
            return response;
        }
    }
}