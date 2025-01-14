using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class ExtraService
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public string? Pershkrim { get; set; }
}
