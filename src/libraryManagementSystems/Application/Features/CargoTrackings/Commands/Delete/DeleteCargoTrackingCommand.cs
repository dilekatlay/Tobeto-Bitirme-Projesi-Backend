using Application.Features.CargoTrackings.Constants;
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

namespace Application.Features.CargoTrackings.Commands.Delete;

public class DeleteCargoTrackingCommand : IRequest<DeletedCargoTrackingResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, CargoTrackingsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCargoTrackings"];

    public class DeleteCargoTrackingCommandHandler : IRequestHandler<DeleteCargoTrackingCommand, DeletedCargoTrackingResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICargoTrackingRepository _cargoTrackingRepository;
        private readonly CargoTrackingBusinessRules _cargoTrackingBusinessRules;

        public DeleteCargoTrackingCommandHandler(IMapper mapper, ICargoTrackingRepository cargoTrackingRepository,
                                         CargoTrackingBusinessRules cargoTrackingBusinessRules)
        {
            _mapper = mapper;
            _cargoTrackingRepository = cargoTrackingRepository;
            _cargoTrackingBusinessRules = cargoTrackingBusinessRules;
        }

        public async Task<DeletedCargoTrackingResponse> Handle(DeleteCargoTrackingCommand request, CancellationToken cancellationToken)
        {
            CargoTracking? cargoTracking = await _cargoTrackingRepository.GetAsync(predicate: ct => ct.Id == request.Id, cancellationToken: cancellationToken);
            await _cargoTrackingBusinessRules.CargoTrackingShouldExistWhenSelected(cargoTracking);

            await _cargoTrackingRepository.DeleteAsync(cargoTracking!);

            DeletedCargoTrackingResponse response = _mapper.Map<DeletedCargoTrackingResponse>(cargoTracking);
            return response;
        }
    }
}