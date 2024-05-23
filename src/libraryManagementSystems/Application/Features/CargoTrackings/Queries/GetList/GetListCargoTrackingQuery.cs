using Application.Features.CargoTrackings.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.CargoTrackings.Constants.CargoTrackingsOperationClaims;

namespace Application.Features.CargoTrackings.Queries.GetList;

public class GetListCargoTrackingQuery : IRequest<GetListResponse<GetListCargoTrackingListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCargoTrackings({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCargoTrackings";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCargoTrackingQueryHandler : IRequestHandler<GetListCargoTrackingQuery, GetListResponse<GetListCargoTrackingListItemDto>>
    {
        private readonly ICargoTrackingRepository _cargoTrackingRepository;
        private readonly IMapper _mapper;

        public GetListCargoTrackingQueryHandler(ICargoTrackingRepository cargoTrackingRepository, IMapper mapper)
        {
            _cargoTrackingRepository = cargoTrackingRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCargoTrackingListItemDto>> Handle(GetListCargoTrackingQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CargoTracking> cargoTrackings = await _cargoTrackingRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCargoTrackingListItemDto> response = _mapper.Map<GetListResponse<GetListCargoTrackingListItemDto>>(cargoTrackings);
            return response;
        }
    }
}