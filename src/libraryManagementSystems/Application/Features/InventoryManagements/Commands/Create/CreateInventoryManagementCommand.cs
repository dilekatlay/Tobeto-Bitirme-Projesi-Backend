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

namespace Application.Features.InventoryManagements.Commands.Create;

public class CreateInventoryManagementCommand : IRequest<CreatedInventoryManagementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookId { get; set; }
    public Guid ShelfId { get; set; }
    public Guid CategoryId { get; set; }

    public string[] Roles => [Admin, Write, InventoryManagementsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetInventoryManagements"];

    public class CreateInventoryManagementCommandHandler : IRequestHandler<CreateInventoryManagementCommand, CreatedInventoryManagementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IInventoryManagementRepository _inventoryManagementRepository;
        private readonly InventoryManagementBusinessRules _inventoryManagementBusinessRules;

        public CreateInventoryManagementCommandHandler(IMapper mapper, IInventoryManagementRepository inventoryManagementRepository,
                                         InventoryManagementBusinessRules inventoryManagementBusinessRules)
        {
            _mapper = mapper;
            _inventoryManagementRepository = inventoryManagementRepository;
            _inventoryManagementBusinessRules = inventoryManagementBusinessRules;
        }

        public async Task<CreatedInventoryManagementResponse> Handle(CreateInventoryManagementCommand request, CancellationToken cancellationToken)
        {
            InventoryManagement inventoryManagement = _mapper.Map<InventoryManagement>(request);

            await _inventoryManagementRepository.AddAsync(inventoryManagement);

            CreatedInventoryManagementResponse response = _mapper.Map<CreatedInventoryManagementResponse>(inventoryManagement);
            return response;
        }
    }
}