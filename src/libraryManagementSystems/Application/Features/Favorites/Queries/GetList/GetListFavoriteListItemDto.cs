using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Favorites.Queries.GetList;

public class GetListFavoriteListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}