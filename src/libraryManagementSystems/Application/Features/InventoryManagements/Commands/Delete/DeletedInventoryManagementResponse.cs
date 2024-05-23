using NArchitecture.Core.Application.Responses;

namespace Application.Features.InventoryManagements.Commands.Delete;

public class DeletedInventoryManagementResponse : IResponse
{
    public Guid Id { get; set; }
}