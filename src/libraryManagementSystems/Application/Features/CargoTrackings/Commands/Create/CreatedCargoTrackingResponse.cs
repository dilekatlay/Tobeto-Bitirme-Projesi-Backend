using NArchitecture.Core.Application.Responses;

namespace Application.Features.CargoTrackings.Commands.Create;

public class CreatedCargoTrackingResponse : IResponse
{
    public Guid Id { get; set; }
    public string CargoTrackingNo { get; set; }
}