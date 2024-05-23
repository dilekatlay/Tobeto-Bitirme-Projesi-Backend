using Application.Features.CargoInformations.Constants;
using Application.Features.CargoInformations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.CargoInformations.Constants.CargoInformationsOperationClaims;

namespace Application.Features.CargoInformations.Queries.GetById;

public class GetByIdCargoInformationQuery : IRequest<GetByIdCargoInformationResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdCargoInformationQueryHandler : IRequestHandler<GetByIdCargoInformationQuery, GetByIdCargoInformationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICargoInformationRepository _cargoInformationRepository;
        private readonly CargoInformationBusinessRules _cargoInformationBusinessRules;

        public GetByIdCargoInformationQueryHandler(IMapper mapper, ICargoInformationRepository cargoInformationRepository, CargoInformationBusinessRules cargoInformationBusinessRules)
        {
            _mapper = mapper;
            _cargoInformationRepository = cargoInformationRepository;
            _cargoInformationBusinessRules = cargoInformationBusinessRules;
        }

        public async Task<GetByIdCargoInformationResponse> Handle(GetByIdCargoInformationQuery request, CancellationToken cancellationToken)
        {
            CargoInformation? cargoInformation = await _cargoInformationRepository.GetAsync(predicate: ci => ci.Id == request.Id, cancellationToken: cancellationToken);
            await _cargoInformationBusinessRules.CargoInformationShouldExistWhenSelected(cargoInformation);

            GetByIdCargoInformationResponse response = _mapper.Map<GetByIdCargoInformationResponse>(cargoInformation);
            return response;
        }
    }
}