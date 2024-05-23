using NArchitecture.Core.Application.Responses;

namespace Application.Features.CargoInformations.Commands.Delete;

public class DeletedCargoInformationResponse : IResponse
{
    public Guid Id { get; set; }
}