using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Order : Entity<Guid>
{
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual Book Book { get; set; }
    public virtual Member Member { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatu OrderStatu { get; set; }
}
