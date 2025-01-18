﻿using System;
using System.Collections.Generic;

namespace hotelManagamentFinal.Models3;

public partial class User
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public string Mbiemer { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Rezervim> Rezervims { get; set; } = new List<Rezervim>();
}
