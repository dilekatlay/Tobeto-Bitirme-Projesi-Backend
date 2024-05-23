using NArchitecture.Core.Application.Dtos;

namespace Application.Features.CargoTrackings.Queries.GetList;

public class GetListCargoTrackingListItemDto : IDto
{
    public Guid Id { get; set; }
    public string CargoTrackingNo { get; set; }
}