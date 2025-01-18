using System;
using System.Collections.Generic;

namespace hotelManagamentFinal.Models3;

public partial class ExtraService
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public string? Pershkrim { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }
}
