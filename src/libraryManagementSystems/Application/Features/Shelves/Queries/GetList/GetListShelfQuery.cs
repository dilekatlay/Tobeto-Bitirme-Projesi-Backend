using Application.Features.Shelves.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Shelves.Constants.ShelvesOperationClaims;

namespace Application.Features.Shelves.Queries.GetList;

public class GetListShelfQuery : IRequest<GetListResponse<GetListShelfListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListShelves({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetShelves";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListShelfQueryHandler : IRequestHandler<GetListShelfQuery, GetListResponse<GetListShelfListItemDto>>
    {
        private readonly IShelfRepository _shelfRepository;
        private readonly IMapper _mapper;

        public GetListShelfQueryHandler(IShelfRepository shelfRepository, IMapper mapper)
        {
            _shelfRepository = shelfRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListShelfListItemDto>> Handle(GetListShelfQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Shelf> shelves = await _shelfRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListShelfListItemDto> response = _mapper.Map<GetListResponse<GetListShelfListItemDto>>(shelves);
            return response;
        }
    }
}