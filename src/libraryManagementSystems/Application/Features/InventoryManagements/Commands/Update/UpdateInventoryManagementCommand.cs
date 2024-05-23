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
using Domain.Enums;

namespace Application.Features.InventoryManagements.Commands.Update;

public class UpdateInventoryManagementCommand : IRequest<UpdatedInventoryManagementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid ShelfId { get; set; }
    public Guid CategoryId { get; set; }
    

    public string[] Roles => [Admin, Write, InventoryManagementsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetInventoryManagements"];

    public class UpdateInventoryManagementCommandHandler : IRequestHandler<UpdateInventoryManagementCommand, UpdatedInventoryManagementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IInventoryManagementRepository _inventoryManagementRepository;
        private readonly InventoryManagementBusinessRules _inventoryManagementBusinessRules;

        public UpdateInventoryManagementCommandHandler(IMapper mapper, IInventoryManagementRepository inventoryManagementRepository,
                                         InventoryManagementBusinessRules inventoryManagementBusinessRules)
        {
            _mapper = mapper;
            _inventoryManagementRepository = inventoryManagementRepository;
            _inventoryManagementBusinessRules = inventoryManagementBusinessRules;
        }

        public async Task<UpdatedInventoryManagementResponse> Handle(UpdateInventoryManagementCommand request, CancellationToken cancellationToken)
        {
            InventoryManagement? inventoryManagement = await _inventoryManagementRepository.GetAsync(predicate: im => im.Id == request.Id, cancellationToken: cancellationToken);
            await _inventoryManagementBusinessRules.InventoryManagementShouldExistWhenSelected(inventoryManagement);
            inventoryManagement = _mapper.Map(request, inventoryManagement);

            await _inventoryManagementRepository.UpdateAsync(inventoryManagement!);

            UpdatedInventoryManagementResponse response = _mapper.Map<UpdatedInventoryManagementResponse>(inventoryManagement);
            return response;
        }
    }
}