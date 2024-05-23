using Application.Features.Managements.Constants;
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

namespace Application.Features.Managements.Commands.Delete;

public class DeleteManagementCommand : IRequest<DeletedManagementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ManagementsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetManagements"];

    public class DeleteManagementCommandHandler : IRequestHandler<DeleteManagementCommand, DeletedManagementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManagementRepository _managementRepository;
        private readonly ManagementBusinessRules _managementBusinessRules;

        public DeleteManagementCommandHandler(IMapper mapper, IManagementRepository managementRepository,
                                         ManagementBusinessRules managementBusinessRules)
        {
            _mapper = mapper;
            _managementRepository = managementRepository;
            _managementBusinessRules = managementBusinessRules;
        }

        public async Task<DeletedManagementResponse> Handle(DeleteManagementCommand request, CancellationToken cancellationToken)
        {
            Management? management = await _managementRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _managementBusinessRules.ManagementShouldExistWhenSelected(management);

            await _managementRepository.DeleteAsync(management!);

            DeletedManagementResponse response = _mapper.Map<DeletedManagementResponse>(management);
            return response;
        }
    }
}