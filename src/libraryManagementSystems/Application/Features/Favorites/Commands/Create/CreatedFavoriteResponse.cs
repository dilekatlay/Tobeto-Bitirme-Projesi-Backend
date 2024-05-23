using NArchitecture.Core.Application.Responses;

namespace Application.Features.Favorites.Commands.Create;

public class CreatedFavoriteResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}