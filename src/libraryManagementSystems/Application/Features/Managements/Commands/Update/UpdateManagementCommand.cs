using Application.Features.Managements.Constants;
using Application.Features.Managements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Managements.Constants.ManagementsOperationClaims;

namespace Application.Features.Managements.Commands.Update;

public class UpdateManagementCommand : IRequest<UpdatedManagementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid InventoryId { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }

    public string[] Roles => [Admin, Write, ManagementsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetManagements"];

    public class UpdateManagementCommandHandler : IRequestHandler<UpdateManagementCommand, UpdatedManagementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManagementRepository _managementRepository;
        private readonly ManagementBusinessRules _managementBusinessRules;

        public UpdateManagementCommandHandler(IMapper mapper, IManagementRepository managementRepository,
                                         ManagementBusinessRules managementBusinessRules)
        {
            _mapper = mapper;
            _managementRepository = managementRepository;
            _managementBusinessRules = managementBusinessRules;
        }

        public async Task<UpdatedManagementResponse> Handle(UpdateManagementCommand request, CancellationToken cancellationToken)
        {
            Management? management = await _managementRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _managementBusinessRules.ManagementShouldExistWhenSelected(management);
            management = _mapper.Map(request, management);

            await _managementRepository.UpdateAsync(management!);

            UpdatedManagementResponse response = _mapper.Map<UpdatedManagementResponse>(management);
            return response;
        }
    }
}