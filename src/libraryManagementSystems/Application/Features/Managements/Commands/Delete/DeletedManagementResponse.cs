using NArchitecture.Core.Application.Responses;

namespace Application.Features.Managements.Commands.Delete;

public class DeletedManagementResponse : IResponse
{
    public Guid Id { get; set; }
}