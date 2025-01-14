using System;
using System.Collections.Generic;
namespace hotelManagement.DAL.Persistence.Entities;


public partial class ExtraService : BaseEntity<int>
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public string? Pershkrim { get; set; }
}
