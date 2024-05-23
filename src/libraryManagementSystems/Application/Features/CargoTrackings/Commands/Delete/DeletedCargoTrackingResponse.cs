using NArchitecture.Core.Application.Responses;

namespace Application.Features.CargoTrackings.Commands.Delete;

public class DeletedCargoTrackingResponse : IResponse
{
    public Guid Id { get; set; }
}