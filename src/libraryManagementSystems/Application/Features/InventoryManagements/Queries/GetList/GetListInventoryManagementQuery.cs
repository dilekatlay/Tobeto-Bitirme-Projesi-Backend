using Application.Features.InventoryManagements.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.InventoryManagements.Constants.InventoryManagementsOperationClaims;

namespace Application.Features.InventoryManagements.Queries.GetList;

public class GetListInventoryManagementQuery : IRequest<GetListResponse<GetListInventoryManagementListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListInventoryManagements({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetInventoryManagements";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListInventoryManagementQueryHandler : IRequestHandler<GetListInventoryManagementQuery, GetListResponse<GetListInventoryManagementListItemDto>>
    {
        private readonly IInventoryManagementRepository _inventoryManagementRepository;
        private readonly IMapper _mapper;

        public GetListInventoryManagementQueryHandler(IInventoryManagementRepository inventoryManagementRepository, IMapper mapper)
        {
            _inventoryManagementRepository = inventoryManagementRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListInventoryManagementListItemDto>> Handle(GetListInventoryManagementQuery request, CancellationToken cancellationToken)
        {
            IPaginate<InventoryManagement> inventoryManagements = await _inventoryManagementRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListInventoryManagementListItemDto> response = _mapper.Map<GetListResponse<GetListInventoryManagementListItemDto>>(inventoryManagements);
            return response;
        }
    }
}