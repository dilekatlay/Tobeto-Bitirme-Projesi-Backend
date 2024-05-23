using Domain.Enums;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Orders.Commands.Update;

public class UpdatedOrderResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatu OrderStatu { get; set; }
}