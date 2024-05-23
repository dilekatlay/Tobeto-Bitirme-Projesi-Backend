using NArchitecture.Core.Application.Responses;

namespace Application.Features.Favorites.Queries.GetById;

public class GetByIdFavoriteResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}