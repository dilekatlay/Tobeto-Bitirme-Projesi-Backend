using Application.Features.CreditCarts.Constants;
using Application.Features.CreditCarts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.CreditCarts.Constants.CreditCartsOperationClaims;

namespace Application.Features.CreditCarts.Commands.Create;

public class CreateCreditCartCommand : IRequest<CreatedCreditCartResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string NameOnTheCart { get; set; }
    public string CartNo { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Cvv { get; set; }

    public string[] Roles => [Admin, Write, CreditCartsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCreditCarts"];

    public class CreateCreditCartCommandHandler : IRequestHandler<CreateCreditCartCommand, CreatedCreditCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICreditCartRepository _creditCartRepository;
        private readonly CreditCartBusinessRules _creditCartBusinessRules;

        public CreateCreditCartCommandHandler(IMapper mapper, ICreditCartRepository creditCartRepository,
                                         CreditCartBusinessRules creditCartBusinessRules)
        {
            _mapper = mapper;
            _creditCartRepository = creditCartRepository;
            _creditCartBusinessRules = creditCartBusinessRules;
        }

        public async Task<CreatedCreditCartResponse> Handle(CreateCreditCartCommand request, CancellationToken cancellationToken)
        {
            CreditCart creditCart = _mapper.Map<CreditCart>(request);

            await _creditCartRepository.AddAsync(creditCart);

            CreatedCreditCartResponse response = _mapper.Map<CreatedCreditCartResponse>(creditCart);
            return response;
        }
    }
}