using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.InventoryManagements.Queries.GetList;

public class GetListInventoryManagementListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid ShelfId { get; set; }
    public Guid CategoryId { get; set; }
}