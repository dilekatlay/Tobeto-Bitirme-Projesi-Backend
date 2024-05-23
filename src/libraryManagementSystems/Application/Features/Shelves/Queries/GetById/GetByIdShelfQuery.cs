using Application.Features.Shelves.Constants;
using Application.Features.Shelves.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Shelves.Constants.ShelvesOperationClaims;

namespace Application.Features.Shelves.Queries.GetById;

public class GetByIdShelfQuery : IRequest<GetByIdShelfResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdShelfQueryHandler : IRequestHandler<GetByIdShelfQuery, GetByIdShelfResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShelfRepository _shelfRepository;
        private readonly ShelfBusinessRules _shelfBusinessRules;

        public GetByIdShelfQueryHandler(IMapper mapper, IShelfRepository shelfRepository, ShelfBusinessRules shelfBusinessRules)
        {
            _mapper = mapper;
            _shelfRepository = shelfRepository;
            _shelfBusinessRules = shelfBusinessRules;
        }

        public async Task<GetByIdShelfResponse> Handle(GetByIdShelfQuery request, CancellationToken cancellationToken)
        {
            Shelf? shelf = await _shelfRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _shelfBusinessRules.ShelfShouldExistWhenSelected(shelf);

            GetByIdShelfResponse response = _mapper.Map<GetByIdShelfResponse>(shelf);
            return response;
        }
    }
}