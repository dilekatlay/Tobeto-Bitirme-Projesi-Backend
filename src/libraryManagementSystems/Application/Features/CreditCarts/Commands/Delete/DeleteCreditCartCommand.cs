using Application.Features.CreditCarts.Constants;
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

namespace Application.Features.CreditCarts.Commands.Delete;

public class DeleteCreditCartCommand : IRequest<DeletedCreditCartResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, CreditCartsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCreditCarts"];

    public class DeleteCreditCartCommandHandler : IRequestHandler<DeleteCreditCartCommand, DeletedCreditCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICreditCartRepository _creditCartRepository;
        private readonly CreditCartBusinessRules _creditCartBusinessRules;

        public DeleteCreditCartCommandHandler(IMapper mapper, ICreditCartRepository creditCartRepository,
                                         CreditCartBusinessRules creditCartBusinessRules)
        {
            _mapper = mapper;
            _creditCartRepository = creditCartRepository;
            _creditCartBusinessRules = creditCartBusinessRules;
        }

        public async Task<DeletedCreditCartResponse> Handle(DeleteCreditCartCommand request, CancellationToken cancellationToken)
        {
            CreditCart? creditCart = await _creditCartRepository.GetAsync(predicate: cc => cc.Id == request.Id, cancellationToken: cancellationToken);
            await _creditCartBusinessRules.CreditCartShouldExistWhenSelected(creditCart);

            await _creditCartRepository.DeleteAsync(creditCart!);

            DeletedCreditCartResponse response = _mapper.Map<DeletedCreditCartResponse>(creditCart);
            return response;
        }
    }
}