using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Members.Queries.GetList;

public class GetListMemberListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }
    public string Adress { get; set; }
    public double PenaltyAmount { get; set; }
    public Guid UserId { get; set; }
}