using NArchitecture.Core.Application.Responses;

namespace Application.Features.Members.Queries.GetById;

public class GetByUserIdMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }
    public string Adress { get; set; }
    public double PenaltyAmount { get; set; }
    public Guid UserId { get; set; }
}