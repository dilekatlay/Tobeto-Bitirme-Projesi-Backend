using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.InventoryManagements.Commands.Update;

public class UpdatedInventoryManagementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid ShelfId { get; set; }
    public Guid CategoryId { get; set; }

}