using NArchitecture.Core.Application.Responses;

namespace Application.Features.Managements.Commands.Update;

public class UpdatedManagementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid InventoryId { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
}