﻿using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class CargoTracking : Entity<Guid>
{
    public string CargoTrackingNo { get; set; }
}
