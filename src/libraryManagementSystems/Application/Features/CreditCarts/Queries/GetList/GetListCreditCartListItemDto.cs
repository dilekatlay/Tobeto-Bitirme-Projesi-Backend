using NArchitecture.Core.Application.Dtos;

namespace Application.Features.CreditCarts.Queries.GetList;

public class GetListCreditCartListItemDto : IDto
{
    public Guid Id { get; set; }
    public string NameOnTheCart { get; set; }
    public string CartNo { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Cvv { get; set; }
}