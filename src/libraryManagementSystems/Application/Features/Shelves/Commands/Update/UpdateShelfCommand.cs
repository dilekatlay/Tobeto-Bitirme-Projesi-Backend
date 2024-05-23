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

namespace Application.Features.Shelves.Commands.Update;

public class UpdateShelfCommand : IRequest<UpdatedShelfResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public int ShelfNo { get; set; }
    public string ShelfLocation { get; set; }
    public int Capacity { get; set; }
    public bool NumberOfBooksAvailable { get; set; }


    public string[] Roles => [Admin, Write, ShelvesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetShelves"];

    public class UpdateShelfCommandHandler : IRequestHandler<UpdateShelfCommand, UpdatedShelfResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShelfRepository _shelfRepository;
        private readonly ShelfBusinessRules _shelfBusinessRules;

        public UpdateShelfCommandHandler(IMapper mapper, IShelfRepository shelfRepository,
                                         ShelfBusinessRules shelfBusinessRules)
        {
            _mapper = mapper;
            _shelfRepository = shelfRepository;
            _shelfBusinessRules = shelfBusinessRules;
        }

        public async Task<UpdatedShelfResponse> Handle(UpdateShelfCommand request, CancellationToken cancellationToken)
        {
            Shelf? shelf = await _shelfRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _shelfBusinessRules.ShelfShouldExistWhenSelected(shelf);
            shelf = _mapper.Map(request, shelf);

            await _shelfRepository.UpdateAsync(shelf!);

            UpdatedShelfResponse response = _mapper.Map<UpdatedShelfResponse>(shelf);
            return response;
        }
    }
}