using Application.Features.CargoInformations.Constants;
using Application.Features.CargoInformations.Constants;
using Application.Features.CargoInformations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.CargoInformations.Constants.CargoInformationsOperationClaims;

namespace Application.Features.CargoInformations.Commands.Delete;

public class DeleteCargoInformationCommand : IRequest<DeletedCargoInformationResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, CargoInformationsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCargoInformations"];

    public class DeleteCargoInformationCommandHandler : IRequestHandler<DeleteCargoInformationCommand, DeletedCargoInformationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICargoInformationRepository _cargoInformationRepository;
        private readonly CargoInformationBusinessRules _cargoInformationBusinessRules;

        public DeleteCargoInformationCommandHandler(IMapper mapper, ICargoInformationRepository cargoInformationRepository,
                                         CargoInformationBusinessRules cargoInformationBusinessRules)
        {
            _mapper = mapper;
            _cargoInformationRepository = cargoInformationRepository;
            _cargoInformationBusinessRules = cargoInformationBusinessRules;
        }

        public async Task<DeletedCargoInformationResponse> Handle(DeleteCargoInformationCommand request, CancellationToken cancellationToken)
        {
            CargoInformation? cargoInformation = await _cargoInformationRepository.GetAsync(predicate: ci => ci.Id == request.Id, cancellationToken: cancellationToken);
            await _cargoInformationBusinessRules.CargoInformationShouldExistWhenSelected(cargoInformation);

            await _cargoInformationRepository.DeleteAsync(cargoInformation!);

            DeletedCargoInformationResponse response = _mapper.Map<DeletedCargoInformationResponse>(cargoInformation);
            return response;
        }
    }
}