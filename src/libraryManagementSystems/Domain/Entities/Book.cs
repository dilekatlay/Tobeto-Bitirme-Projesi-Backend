using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Book: Entity<Guid>
{
    public string ISBNNo { get; set; }
    public string BookName { get; set; }
    public string Summary {  get; set; }
    public string Writer { get; set; }
    public string imageUrl { get; set; }
    public int NumberOfCopies { get; set; }
    public int NumberOfPages { get; set; }
    public decimal UnitPrice { get; set; }
    public Guid CategoryId {  get; set; }
    public Guid ShelfId { get; set; }

    public virtual Category Category { get; set; }
    public virtual Shelf Shelf { get; set; }
    
}
