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

namespace Application.Features.CreditCarts.Commands.Update;

public class UpdateCreditCartCommand : IRequest<UpdatedCreditCartResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string NameOnTheCart { get; set; }
    public string CartNo { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Cvv { get; set; }

    public string[] Roles => [Admin, Write, CreditCartsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCreditCarts"];

    public class UpdateCreditCartCommandHandler : IRequestHandler<UpdateCreditCartCommand, UpdatedCreditCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICreditCartRepository _creditCartRepository;
        private readonly CreditCartBusinessRules _creditCartBusinessRules;

        public UpdateCreditCartCommandHandler(IMapper mapper, ICreditCartRepository creditCartRepository,
                                         CreditCartBusinessRules creditCartBusinessRules)
        {
            _mapper = mapper;
            _creditCartRepository = creditCartRepository;
            _creditCartBusinessRules = creditCartBusinessRules;
        }

        public async Task<UpdatedCreditCartResponse> Handle(UpdateCreditCartCommand request, CancellationToken cancellationToken)
        {
            CreditCart? creditCart = await _creditCartRepository.GetAsync(predicate: cc => cc.Id == request.Id, cancellationToken: cancellationToken);
            await _creditCartBusinessRules.CreditCartShouldExistWhenSelected(creditCart);
            creditCart = _mapper.Map(request, creditCart);

            await _creditCartRepository.UpdateAsync(creditCart!);

            UpdatedCreditCartResponse response = _mapper.Map<UpdatedCreditCartResponse>(creditCart);
            return response;
        }
    }
}