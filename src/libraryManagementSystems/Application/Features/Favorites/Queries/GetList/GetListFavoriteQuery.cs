using Application.Features.Favorites.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Favorites.Constants.FavoritesOperationClaims;

namespace Application.Features.Favorites.Queries.GetList;

public class GetListFavoriteQuery : IRequest<GetListResponse<GetListFavoriteListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListFavoriteQueryHandler : IRequestHandler<GetListFavoriteQuery, GetListResponse<GetListFavoriteListItemDto>>
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;

        public GetListFavoriteQueryHandler(IFavoriteRepository favoriteRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFavoriteListItemDto>> Handle(GetListFavoriteQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Favorite> favorites = await _favoriteRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFavoriteListItemDto> response = _mapper.Map<GetListResponse<GetListFavoriteListItemDto>>(favorites);
            return response;
        }
    }
}