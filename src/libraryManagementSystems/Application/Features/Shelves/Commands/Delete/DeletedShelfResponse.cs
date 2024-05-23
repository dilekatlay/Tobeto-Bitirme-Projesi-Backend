using NArchitecture.Core.Application.Responses;

namespace Application.Features.Shelves.Commands.Delete;

public class DeletedShelfResponse : IResponse
{
    public Guid Id { get; set; }
}