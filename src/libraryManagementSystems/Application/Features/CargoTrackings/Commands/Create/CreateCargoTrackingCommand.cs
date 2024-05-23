using Application.Features.CargoTrackings.Constants;
using Application.Features.CargoTrackings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.CargoTrackings.Constants.CargoTrackingsOperationClaims;

namespace Application.Features.CargoTrackings.Commands.Create;

public class CreateCargoTrackingCommand : IRequest<CreatedCargoTrackingResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string CargoTrackingNo { get; set; }

    public string[] Roles => [Admin, Write, CargoTrackingsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCargoTrackings"];

    public class CreateCargoTrackingCommandHandler : IRequestHandler<CreateCargoTrackingCommand, CreatedCargoTrackingResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICargoTrackingRepository _cargoTrackingRepository;
        private readonly CargoTrackingBusinessRules _cargoTrackingBusinessRules;

        public CreateCargoTrackingCommandHandler(IMapper mapper, ICargoTrackingRepository cargoTrackingRepository,
                                         CargoTrackingBusinessRules cargoTrackingBusinessRules)
        {
            _mapper = mapper;
            _cargoTrackingRepository = cargoTrackingRepository;
            _cargoTrackingBusinessRules = cargoTrackingBusinessRules;
        }

        public async Task<CreatedCargoTrackingResponse> Handle(CreateCargoTrackingCommand request, CancellationToken cancellationToken)
        {
            CargoTracking cargoTracking = _mapper.Map<CargoTracking>(request);

            await _cargoTrackingRepository.AddAsync(cargoTracking);

            CreatedCargoTrackingResponse response = _mapper.Map<CreatedCargoTrackingResponse>(cargoTracking);
            return response;
        }
    }
}