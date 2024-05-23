using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Favorite : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public virtual Book Book { get; set; }
    public virtual User User { get; set; }
}
