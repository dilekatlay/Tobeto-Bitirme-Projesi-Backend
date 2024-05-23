using Application.Features.InventoryManagements.Constants;
using Application.Features.InventoryManagements.Constants;
using Application.Features.InventoryManagements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.InventoryManagements.Constants.InventoryManagementsOperationClaims;

namespace Application.Features.InventoryManagements.Commands.Delete;

public class DeleteInventoryManagementCommand : IRequest<DeletedInventoryManagementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, InventoryManagementsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetInventoryManagements"];

    public class DeleteInventoryManagementCommandHandler : IRequestHandler<DeleteInventoryManagementCommand, DeletedInventoryManagementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IInventoryManagementRepository _inventoryManagementRepository;
        private readonly InventoryManagementBusinessRules _inventoryManagementBusinessRules;

        public DeleteInventoryManagementCommandHandler(IMapper mapper, IInventoryManagementRepository inventoryManagementRepository,
                                         InventoryManagementBusinessRules inventoryManagementBusinessRules)
        {
            _mapper = mapper;
            _inventoryManagementRepository = inventoryManagementRepository;
            _inventoryManagementBusinessRules = inventoryManagementBusinessRules;
        }

        public async Task<DeletedInventoryManagementResponse> Handle(DeleteInventoryManagementCommand request, CancellationToken cancellationToken)
        {
            InventoryManagement? inventoryManagement = await _inventoryManagementRepository.GetAsync(predicate: im => im.Id == request.Id, cancellationToken: cancellationToken);
            await _inventoryManagementBusinessRules.InventoryManagementShouldExistWhenSelected(inventoryManagement);

            await _inventoryManagementRepository.DeleteAsync(inventoryManagement!);

            DeletedInventoryManagementResponse response = _mapper.Map<DeletedInventoryManagementResponse>(inventoryManagement);
            return response;
        }
    }
}