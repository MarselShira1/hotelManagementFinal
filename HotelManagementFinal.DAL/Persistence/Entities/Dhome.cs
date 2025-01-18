using System;
using System.Collections.Generic;

namespace hotelManagamentFinal.Models3;

public partial class Dhome
{
    public int Id { get; set; }

    public int TipDhome { get; set; }

    public int? Kat { get; set; }

    public string NumerDhome { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Rezervim> Rezervims { get; set; } = new List<Rezervim>();

    public virtual TipDhome TipDhomeNavigation { get; set; } = null!;
}
