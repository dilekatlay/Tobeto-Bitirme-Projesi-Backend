using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class LoanTransaction : Entity<Guid>
{
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public Guid BookId { get; set; }
    //public Guid MemberID { get; set; }
    public virtual Book Book { get; set; }
    
    //public virtual Member Members { get; set; }
}
