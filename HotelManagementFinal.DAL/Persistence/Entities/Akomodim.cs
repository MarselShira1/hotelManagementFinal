using System;
using System.Collections.Generic;

namespace hotelManagamentFinal.Models3;

public partial class Akomodim
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
