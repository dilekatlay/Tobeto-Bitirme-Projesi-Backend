using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class CreditCart : Entity<Guid>
{
    public string NameOnTheCart { get; set; }
    public string CartNo { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Cvv {  get; set; }
}
