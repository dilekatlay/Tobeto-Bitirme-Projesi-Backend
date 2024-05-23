using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Management : Entity<Guid>
{
    public Guid InventoryId { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }

    public virtual InventoryManagement InventoryManagement { get; set; }
    public virtual Book Book { get; set; }
    public virtual User User { get; set; }


}
