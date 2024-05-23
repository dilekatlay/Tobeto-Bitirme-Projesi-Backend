using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Shelf: Entity<Guid>
{
    public int ShelfNo { get; set; }
    public string ShelfLocation { get; set; }
    public int Capacity {  get; set; }
    public bool NumberOfBooksAvailable { get; set; }

}
