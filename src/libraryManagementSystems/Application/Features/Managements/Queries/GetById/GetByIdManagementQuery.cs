using Application.Features.Managements.Constants;
using Application.Features.Managements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Managements.Constants.ManagementsOperationClaims;

namespace Application.Features.Managements.Queries.GetById;

public class GetByIdManagementQuery : IRequest<GetByIdManagementResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdManagementQueryHandler : IRequestHandler<GetByIdManagementQuery, GetByIdManagementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManagementRepository _managementRepository;
        private readonly ManagementBusinessRules _managementBusinessRules;

        public GetByIdManagementQueryHandler(IMapper mapper, IManagementRepository managementRepository, ManagementBusinessRules managementBusinessRules)
        {
            _mapper = mapper;
            _managementRepository = managementRepository;
            _managementBusinessRules = managementBusinessRules;
        }

        public async Task<GetByIdManagementResponse> Handle(GetByIdManagementQuery request, CancellationToken cancellationToken)
        {
            Management? management = await _managementRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _managementBusinessRules.ManagementShouldExistWhenSelected(management);

            GetByIdManagementResponse response = _mapper.Map<GetByIdManagementResponse>(management);
            return response;
        }
    }
}