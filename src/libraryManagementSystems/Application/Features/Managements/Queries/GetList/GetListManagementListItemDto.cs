using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Managements.Queries.GetList;

public class GetListManagementListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid InventoryId { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
}