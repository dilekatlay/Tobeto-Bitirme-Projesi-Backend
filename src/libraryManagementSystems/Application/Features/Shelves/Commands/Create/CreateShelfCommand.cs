using Application.Features.Shelves.Constants;
using Application.Features.Shelves.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Shelves.Constants.ShelvesOperationClaims;
using Domain.Enums;

namespace Application.Features.Shelves.Commands.Create;

public class CreateShelfCommand : IRequest<CreatedShelfResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int ShelfNo { get; set; }
    public string ShelfLocation { get; set; }
    public int Capacity { get; set; }
    public bool NumberOfBooksAvailable { get; set; }


    public string[] Roles => [Admin, Write, ShelvesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetShelves"];

    public class CreateShelfCommandHandler : IRequestHandler<CreateShelfCommand, CreatedShelfResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShelfRepository _shelfRepository;
        private readonly ShelfBusinessRules _shelfBusinessRules;

        public CreateShelfCommandHandler(IMapper mapper, IShelfRepository shelfRepository,
                                         ShelfBusinessRules shelfBusinessRules)
        {
            _mapper = mapper;
            _shelfRepository = shelfRepository;
            _shelfBusinessRules = shelfBusinessRules;
        }

        public async Task<CreatedShelfResponse> Handle(CreateShelfCommand request, CancellationToken cancellationToken)
        {
            Shelf shelf = _mapper.Map<Shelf>(request);

            await _shelfRepository.AddAsync(shelf);

            CreatedShelfResponse response = _mapper.Map<CreatedShelfResponse>(shelf);
            return response;
        }
    }
}