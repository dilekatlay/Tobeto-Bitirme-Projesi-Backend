using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Member : Entity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }
    public string Adress { get; set; }
    public double PenaltyAmount { get; set; } = 0;
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
