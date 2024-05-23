using Application.Features.Managements.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Managements.Constants.ManagementsOperationClaims;

namespace Application.Features.Managements.Queries.GetList;

public class GetListManagementQuery : IRequest<GetListResponse<GetListManagementListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListManagements({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetManagements";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListManagementQueryHandler : IRequestHandler<GetListManagementQuery, GetListResponse<GetListManagementListItemDto>>
    {
        private readonly IManagementRepository _managementRepository;
        private readonly IMapper _mapper;

        public GetListManagementQueryHandler(IManagementRepository managementRepository, IMapper mapper)
        {
            _managementRepository = managementRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListManagementListItemDto>> Handle(GetListManagementQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Management> managements = await _managementRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListManagementListItemDto> response = _mapper.Map<GetListResponse<GetListManagementListItemDto>>(managements);
            return response;
        }
    }
}