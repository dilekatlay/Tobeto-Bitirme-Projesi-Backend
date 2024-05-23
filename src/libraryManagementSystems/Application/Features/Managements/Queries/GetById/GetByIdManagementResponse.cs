using NArchitecture.Core.Application.Responses;

namespace Application.Features.Managements.Queries.GetById;

public class GetByIdManagementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid InventoryId { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
}