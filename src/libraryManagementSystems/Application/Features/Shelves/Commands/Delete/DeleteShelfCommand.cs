using Application.Features.Shelves.Constants;
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

namespace Application.Features.Shelves.Commands.Delete;

public class DeleteShelfCommand : IRequest<DeletedShelfResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ShelvesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetShelves"];

    public class DeleteShelfCommandHandler : IRequestHandler<DeleteShelfCommand, DeletedShelfResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShelfRepository _shelfRepository;
        private readonly ShelfBusinessRules _shelfBusinessRules;

        public DeleteShelfCommandHandler(IMapper mapper, IShelfRepository shelfRepository,
                                         ShelfBusinessRules shelfBusinessRules)
        {
            _mapper = mapper;
            _shelfRepository = shelfRepository;
            _shelfBusinessRules = shelfBusinessRules;
        }

        public async Task<DeletedShelfResponse> Handle(DeleteShelfCommand request, CancellationToken cancellationToken)
        {
            Shelf? shelf = await _shelfRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _shelfBusinessRules.ShelfShouldExistWhenSelected(shelf);

            await _shelfRepository.DeleteAsync(shelf!);

            DeletedShelfResponse response = _mapper.Map<DeletedShelfResponse>(shelf);
            return response;
        }
    }
}