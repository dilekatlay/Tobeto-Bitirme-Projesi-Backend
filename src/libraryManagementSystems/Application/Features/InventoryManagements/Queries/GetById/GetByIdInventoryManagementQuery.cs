using Application.Features.InventoryManagements.Constants;
using Application.Features.InventoryManagements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.InventoryManagements.Constants.InventoryManagementsOperationClaims;

namespace Application.Features.InventoryManagements.Queries.GetById;

public class GetByIdInventoryManagementQuery : IRequest<GetByIdInventoryManagementResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdInventoryManagementQueryHandler : IRequestHandler<GetByIdInventoryManagementQuery, GetByIdInventoryManagementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IInventoryManagementRepository _inventoryManagementRepository;
        private readonly InventoryManagementBusinessRules _inventoryManagementBusinessRules;

        public GetByIdInventoryManagementQueryHandler(IMapper mapper, IInventoryManagementRepository inventoryManagementRepository, InventoryManagementBusinessRules inventoryManagementBusinessRules)
        {
            _mapper = mapper;
            _inventoryManagementRepository = inventoryManagementRepository;
            _inventoryManagementBusinessRules = inventoryManagementBusinessRules;
        }

        public async Task<GetByIdInventoryManagementResponse> Handle(GetByIdInventoryManagementQuery request, CancellationToken cancellationToken)
        {
            InventoryManagement? inventoryManagement = await _inventoryManagementRepository.GetAsync(predicate: im => im.Id == request.Id, cancellationToken: cancellationToken);
            await _inventoryManagementBusinessRules.InventoryManagementShouldExistWhenSelected(inventoryManagement);

            GetByIdInventoryManagementResponse response = _mapper.Map<GetByIdInventoryManagementResponse>(inventoryManagement);
            return response;
        }
    }
}