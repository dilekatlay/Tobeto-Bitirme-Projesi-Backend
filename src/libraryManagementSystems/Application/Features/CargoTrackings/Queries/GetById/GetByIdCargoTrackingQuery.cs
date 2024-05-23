using Application.Features.CargoTrackings.Constants;
using Application.Features.CargoTrackings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.CargoTrackings.Constants.CargoTrackingsOperationClaims;

namespace Application.Features.CargoTrackings.Queries.GetById;

public class GetByIdCargoTrackingQuery : IRequest<GetByIdCargoTrackingResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdCargoTrackingQueryHandler : IRequestHandler<GetByIdCargoTrackingQuery, GetByIdCargoTrackingResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICargoTrackingRepository _cargoTrackingRepository;
        private readonly CargoTrackingBusinessRules _cargoTrackingBusinessRules;

        public GetByIdCargoTrackingQueryHandler(IMapper mapper, ICargoTrackingRepository cargoTrackingRepository, CargoTrackingBusinessRules cargoTrackingBusinessRules)
        {
            _mapper = mapper;
            _cargoTrackingRepository = cargoTrackingRepository;
            _cargoTrackingBusinessRules = cargoTrackingBusinessRules;
        }

        public async Task<GetByIdCargoTrackingResponse> Handle(GetByIdCargoTrackingQuery request, CancellationToken cancellationToken)
        {
            CargoTracking? cargoTracking = await _cargoTrackingRepository.GetAsync(predicate: ct => ct.Id == request.Id, cancellationToken: cancellationToken);
            await _cargoTrackingBusinessRules.CargoTrackingShouldExistWhenSelected(cargoTracking);

            GetByIdCargoTrackingResponse response = _mapper.Map<GetByIdCargoTrackingResponse>(cargoTracking);
            return response;
        }
    }
}