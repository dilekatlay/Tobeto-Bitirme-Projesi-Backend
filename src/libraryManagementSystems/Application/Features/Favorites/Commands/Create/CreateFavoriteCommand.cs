using Application.Features.Favorites.Constants;
using Application.Features.Favorites.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.Features.Favorites.Constants.FavoritesOperationClaims;

namespace Application.Features.Favorites.Commands.Create;

public class CreateFavoriteCommand : IRequest<CreatedFavoriteResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }

    public string[] Roles => [Admin, Write, FavoritesOperationClaims.Create];

    public class CreateFavoriteCommandHandler : IRequestHandler<CreateFavoriteCommand, CreatedFavoriteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly FavoriteBusinessRules _favoriteBusinessRules;

        public CreateFavoriteCommandHandler(IMapper mapper, IFavoriteRepository favoriteRepository,
                                         FavoriteBusinessRules favoriteBusinessRules)
        {
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _favoriteBusinessRules = favoriteBusinessRules;
        }

        public async Task<CreatedFavoriteResponse> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
        {
            Favorite favorite = _mapper.Map<Favorite>(request);

            await _favoriteRepository.AddAsync(favorite);

            CreatedFavoriteResponse response = _mapper.Map<CreatedFavoriteResponse>(favorite);
            return response;
        }
    }
}