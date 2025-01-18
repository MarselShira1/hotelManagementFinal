using System;
using System.Collections.Generic;

namespace hotelManagamentFinal.Models3;

public partial class Fature
{
    public int Id { get; set; }

    public int Rezervim { get; set; }

    public decimal RoomCharge { get; set; }

    public decimal? ServiceCharge { get; set; }

    public decimal Total { get; set; }

    public string? Status { get; set; }

    public DateOnly DateFature { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Pagese> Pageses { get; set; } = new List<Pagese>();

    public virtual Rezervim RezervimNavigation { get; set; } = null!;
}
