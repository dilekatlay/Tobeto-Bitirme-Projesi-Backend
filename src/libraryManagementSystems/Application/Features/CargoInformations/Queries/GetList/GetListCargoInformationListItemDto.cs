using NArchitecture.Core.Application.Dtos;

namespace Application.Features.CargoInformations.Queries.GetList;

public class GetListCargoInformationListItemDto : IDto
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string Adress { get; set; }
    public string PhoneNumber { get; set; }
}