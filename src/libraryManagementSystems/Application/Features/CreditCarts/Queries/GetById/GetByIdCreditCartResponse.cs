using NArchitecture.Core.Application.Responses;

namespace Application.Features.CreditCarts.Queries.GetById;

public class GetByIdCreditCartResponse : IResponse
{
    public Guid Id { get; set; }
    public string NameOnTheCart { get; set; }
    public string CartNo { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Cvv { get; set; }
}