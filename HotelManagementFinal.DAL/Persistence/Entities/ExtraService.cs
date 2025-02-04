﻿using System;
using System.Collections.Generic;

using hotelManagement.DAL.Persistence.Entities;

namespace hotelManagament.DAL.Persistence.Entities;

public partial class ExtraService : BaseEntity<int>
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public string? Pershkrim { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }
}
