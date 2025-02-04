﻿using System;
using System.Collections.Generic;

using hotelManagement.DAL.Persistence.Entities;

namespace hotelManagament.DAL.Persistence.Entities;

public partial class RezervimService : BaseEntity<int>
{
    public int Id { get; set; }

    public int Rezervim { get; set; }

    public int Sherbim { get; set; }

    public int Sasi { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual Rezervim RezervimNavigation { get; set; } = null!;
}
