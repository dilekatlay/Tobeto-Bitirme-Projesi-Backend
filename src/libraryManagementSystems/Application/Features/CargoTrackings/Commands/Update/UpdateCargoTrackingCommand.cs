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

namespace Application.Features.CargoTrackings.Commands.Update;

public class UpdateCargoTrackingCommand : IRequest<UpdatedCargoTrackingResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string CargoTrackingNo { get; set; }

    public string[] Roles => [Admin, Write, CargoTrackingsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCargoTrackings"];

    public class UpdateCargoTrackingCommandHandler : IRequestHandler<UpdateCargoTrackingCommand, UpdatedCargoTrackingResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICargoTrackingRepository _cargoTrackingRepository;
        private readonly CargoTrackingBusinessRules _cargoTrackingBusinessRules;

        public UpdateCargoTrackingCommandHandler(IMapper mapper, ICargoTrackingRepository cargoTrackingRepository,
                                         CargoTrackingBusinessRules cargoTrackingBusinessRules)
        {
            _mapper = mapper;
            _cargoTrackingRepository = cargoTrackingRepository;
            _cargoTrackingBusinessRules = cargoTrackingBusinessRules;
        }

        public async Task<UpdatedCargoTrackingResponse> Handle(UpdateCargoTrackingCommand request, CancellationToken cancellationToken)
        {
            CargoTracking? cargoTracking = await _cargoTrackingRepository.GetAsync(predicate: ct => ct.Id == request.Id, cancellationToken: cancellationToken);
            await _cargoTrackingBusinessRules.CargoTrackingShouldExistWhenSelected(cargoTracking);
            cargoTracking = _mapper.Map(request, cargoTracking);

            await _cargoTrackingRepository.UpdateAsync(cargoTracking!);

            UpdatedCargoTrackingResponse response = _mapper.Map<UpdatedCargoTrackingResponse>(cargoTracking);
            return response;
        }
    }
}