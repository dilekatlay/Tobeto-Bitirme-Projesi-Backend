using NArchitecture.Core.Application.Responses;

namespace Application.Features.CargoInformations.Commands.Create;

public class CreatedCargoInformationResponse : IResponse
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string Adress { get; set; }
    public string PhoneNumber { get; set; }
}