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

namespace Application.Features.Managements.Commands.Create;

public class CreateManagementCommand : IRequest<CreatedManagementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid InventoryId { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }

    public string[] Roles => [Admin, Write, ManagementsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetManagements"];

    public class CreateManagementCommandHandler : IRequestHandler<CreateManagementCommand, CreatedManagementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManagementRepository _managementRepository;
        private readonly ManagementBusinessRules _managementBusinessRules;

        public CreateManagementCommandHandler(IMapper mapper, IManagementRepository managementRepository,
                                         ManagementBusinessRules managementBusinessRules)
        {
            _mapper = mapper;
            _managementRepository = managementRepository;
            _managementBusinessRules = managementBusinessRules;
        }

        public async Task<CreatedManagementResponse> Handle(CreateManagementCommand request, CancellationToken cancellationToken)
        {
            Management management = _mapper.Map<Management>(request);

            await _managementRepository.AddAsync(management);

            CreatedManagementResponse response = _mapper.Map<CreatedManagementResponse>(management);
            return response;
        }
    }
}