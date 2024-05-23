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

namespace Application.Features.CargoInformations.Commands.Create;

public class CreateCargoInformationCommand : IRequest<CreatedCargoInformationResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string CompanyName { get; set; }
    public string Adress { get; set; }
    public string PhoneNumber { get; set; }

    public string[] Roles => [Admin, Write, CargoInformationsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCargoInformations"];

    public class CreateCargoInformationCommandHandler : IRequestHandler<CreateCargoInformationCommand, CreatedCargoInformationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICargoInformationRepository _cargoInformationRepository;
        private readonly CargoInformationBusinessRules _cargoInformationBusinessRules;

        public CreateCargoInformationCommandHandler(IMapper mapper, ICargoInformationRepository cargoInformationRepository,
                                         CargoInformationBusinessRules cargoInformationBusinessRules)
        {
            _mapper = mapper;
            _cargoInformationRepository = cargoInformationRepository;
            _cargoInformationBusinessRules = cargoInformationBusinessRules;
        }

        public async Task<CreatedCargoInformationResponse> Handle(CreateCargoInformationCommand request, CancellationToken cancellationToken)
        {
            CargoInformation cargoInformation = _mapper.Map<CargoInformation>(request);

            await _cargoInformationRepository.AddAsync(cargoInformation);

            CreatedCargoInformationResponse response = _mapper.Map<CreatedCargoInformationResponse>(cargoInformation);
            return response;
        }
    }
}