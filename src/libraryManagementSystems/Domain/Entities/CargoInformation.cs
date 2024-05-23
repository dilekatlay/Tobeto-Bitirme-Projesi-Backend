using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class CargoInformation : Entity<Guid>
{
    public string CompanyName { get; set; }
    public string Adress {  get; set; }
    public string PhoneNumber {  get; set; }
}
