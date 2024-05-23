using Domain.Enums;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Orders.Queries.GetList;

public class GetListOrderListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public Guid CargoInformationId { get; set; }
    public Guid CargoTrackingId { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatu OrderStatu { get; set; }
}