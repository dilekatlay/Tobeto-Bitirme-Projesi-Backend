using NArchitecture.Core.Application.Responses;

namespace Application.Features.Managements.Commands.Create;

public class CreatedManagementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid InventoryId { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
}