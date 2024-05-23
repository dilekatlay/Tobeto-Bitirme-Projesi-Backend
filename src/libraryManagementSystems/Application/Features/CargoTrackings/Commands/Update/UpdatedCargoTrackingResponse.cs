using NArchitecture.Core.Application.Responses;

namespace Application.Features.CargoTrackings.Commands.Update;

public class UpdatedCargoTrackingResponse : IResponse
{
    public Guid Id { get; set; }
    public string CargoTrackingNo { get; set; }
}