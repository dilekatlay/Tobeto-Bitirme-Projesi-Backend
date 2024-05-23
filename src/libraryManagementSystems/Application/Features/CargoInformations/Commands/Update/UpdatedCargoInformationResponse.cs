using NArchitecture.Core.Application.Responses;

namespace Application.Features.CargoInformations.Commands.Update;

public class UpdatedCargoInformationResponse : IResponse
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string Adress { get; set; }
    public string PhoneNumber { get; set; }
}