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

namespace Application.Features.CargoInformations.Commands.Update;

public class UpdateCargoInformationCommand : IRequest<UpdatedCargoInformationResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string Adress { get; set; }
    public string PhoneNumber { get; set; }

    public string[] Roles => [Admin, Write, CargoInformationsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCargoInformations"];

    public class UpdateCargoInformationCommandHandler : IRequestHandler<UpdateCargoInformationCommand, UpdatedCargoInformationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICargoInformationRepository _cargoInformationRepository;
        private readonly CargoInformationBusinessRules _cargoInformationBusinessRules;

        public UpdateCargoInformationCommandHandler(IMapper mapper, ICargoInformationRepository cargoInformationRepository,
                                         CargoInformationBusinessRules cargoInformationBusinessRules)
        {
            _mapper = mapper;
            _cargoInformationRepository = cargoInformationRepository;
            _cargoInformationBusinessRules = cargoInformationBusinessRules;
        }

        public async Task<UpdatedCargoInformationResponse> Handle(UpdateCargoInformationCommand request, CancellationToken cancellationToken)
        {
            CargoInformation? cargoInformation = await _cargoInformationRepository.GetAsync(predicate: ci => ci.Id == request.Id, cancellationToken: cancellationToken);
            await _cargoInformationBusinessRules.CargoInformationShouldExistWhenSelected(cargoInformation);
            cargoInformation = _mapper.Map(request, cargoInformation);

            await _cargoInformationRepository.UpdateAsync(cargoInformation!);

            UpdatedCargoInformationResponse response = _mapper.Map<UpdatedCargoInformationResponse>(cargoInformation);
            return response;
        }
    }
}