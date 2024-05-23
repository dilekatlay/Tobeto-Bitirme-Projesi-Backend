using NArchitecture.Core.Application.Responses;

namespace Application.Features.CargoTrackings.Queries.GetById;

public class GetByIdCargoTrackingResponse : IResponse
{
    public Guid Id { get; set; }
    public string CargoTrackingNo { get; set; }
}