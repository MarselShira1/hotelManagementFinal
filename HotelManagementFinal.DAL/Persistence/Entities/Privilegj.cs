﻿using System;
using System.Collections.Generic;
using hotelManagement.DAL.Persistence.Entities;

namespace hotelManagament.DAL.Persistence.Entities;

public partial class Privilegj : BaseEntity<int>

{
    public int Id { get; set; }

    public int Action { get; set; }

    public int Rol { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual Action ActionNavigation { get; set; } = null!;

    public virtual Role RolNavigation { get; set; } = null!;
}
