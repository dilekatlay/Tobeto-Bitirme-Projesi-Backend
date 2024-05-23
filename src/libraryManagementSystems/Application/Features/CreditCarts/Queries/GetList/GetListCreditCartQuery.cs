using Application.Features.CreditCarts.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.CreditCarts.Constants.CreditCartsOperationClaims;

namespace Application.Features.CreditCarts.Queries.GetList;

public class GetListCreditCartQuery : IRequest<GetListResponse<GetListCreditCartListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCreditCarts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCreditCarts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCreditCartQueryHandler : IRequestHandler<GetListCreditCartQuery, GetListResponse<GetListCreditCartListItemDto>>
    {
        private readonly ICreditCartRepository _creditCartRepository;
        private readonly IMapper _mapper;

        public GetListCreditCartQueryHandler(ICreditCartRepository creditCartRepository, IMapper mapper)
        {
            _creditCartRepository = creditCartRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCreditCartListItemDto>> Handle(GetListCreditCartQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CreditCart> creditCarts = await _creditCartRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCreditCartListItemDto> response = _mapper.Map<GetListResponse<GetListCreditCartListItemDto>>(creditCarts);
            return response;
        }
    }
}