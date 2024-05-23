using Application.Features.CargoInformations.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.CargoInformations.Constants.CargoInformationsOperationClaims;

namespace Application.Features.CargoInformations.Queries.GetList;

public class GetListCargoInformationQuery : IRequest<GetListResponse<GetListCargoInformationListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCargoInformations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCargoInformations";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCargoInformationQueryHandler : IRequestHandler<GetListCargoInformationQuery, GetListResponse<GetListCargoInformationListItemDto>>
    {
        private readonly ICargoInformationRepository _cargoInformationRepository;
        private readonly IMapper _mapper;

        public GetListCargoInformationQueryHandler(ICargoInformationRepository cargoInformationRepository, IMapper mapper)
        {
            _cargoInformationRepository = cargoInformationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCargoInformationListItemDto>> Handle(GetListCargoInformationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CargoInformation> cargoInformations = await _cargoInformationRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCargoInformationListItemDto> response = _mapper.Map<GetListResponse<GetListCargoInformationListItemDto>>(cargoInformations);
            return response;
        }
    }
}