﻿using System;
using System.Collections.Generic;
using hotelManagement.DAL.Persistence.Entities;

namespace hotelManagament.DAL.Persistence.Entities;

public partial class Akomodim : BaseEntity<int>

{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public int Adults { get; set; }

    public int? Femije { get; set; }

    public decimal Cmim { get; set; }

    public bool KrevatExtra { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Rezervim> Rezervims { get; set; } = new List<Rezervim>();
}
