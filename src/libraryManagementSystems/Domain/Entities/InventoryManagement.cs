using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class InventoryManagement : Entity<Guid>
{
    public Guid BookId { get; set; }
    public Guid ShelfId { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Book Book { get; set; }
    public virtual Category Category { get; set; }
    public virtual Shelf Shelf { get; set; }
    
}
