using Application.Features.CreditCarts.Constants;
using Application.Features.CreditCarts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.CreditCarts.Constants.CreditCartsOperationClaims;

namespace Application.Features.CreditCarts.Queries.GetById;

public class GetByIdCreditCartQuery : IRequest<GetByIdCreditCartResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdCreditCartQueryHandler : IRequestHandler<GetByIdCreditCartQuery, GetByIdCreditCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICreditCartRepository _creditCartRepository;
        private readonly CreditCartBusinessRules _creditCartBusinessRules;

        public GetByIdCreditCartQueryHandler(IMapper mapper, ICreditCartRepository creditCartRepository, CreditCartBusinessRules creditCartBusinessRules)
        {
            _mapper = mapper;
            _creditCartRepository = creditCartRepository;
            _creditCartBusinessRules = creditCartBusinessRules;
        }

        public async Task<GetByIdCreditCartResponse> Handle(GetByIdCreditCartQuery request, CancellationToken cancellationToken)
        {
            CreditCart? creditCart = await _creditCartRepository.GetAsync(predicate: cc => cc.Id == request.Id, cancellationToken: cancellationToken);
            await _creditCartBusinessRules.CreditCartShouldExistWhenSelected(creditCart);

            GetByIdCreditCartResponse response = _mapper.Map<GetByIdCreditCartResponse>(creditCart);
            return response;
        }
    }
}