﻿using System;
using System.Collections.Generic;

namespace hotelManagement.DAL.Persistence.Entities;

public partial class Rezervim : BaseEntity<int>
{
    public int Id { get; set; }

    public int User { get; set; }

    public int Dhome { get; set; }

    public int RoomRate { get; set; }
    public DateOnly CheckIn { get; set; }

    public DateOnly CheckOut { get; set; }

    public decimal Cmim { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }


    public virtual Dhome DhomeNavigation { get; set; } = null!;

    public virtual ICollection<Fature> Fatures { get; set; } = new List<Fature>();

    public virtual ICollection<RezervimService> RezervimServices { get; set; } = new List<RezervimService>();

    public virtual RoomRate? RoomRateNavigation { get; set; }

    public virtual User UserNavigation { get; set; } = null!;
}
