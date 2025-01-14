using System;
using System.Collections.Generic;
namespace hotelManagement.DAL.Persistence.Entities;

public partial class Review : BaseEntity<int>
{
    public int Id { get; set; }

    public int User { get; set; }

    public int? Rating { get; set; }

    public string? Pershkrim { get; set; }

    public DateOnly Date { get; set; }

    public virtual User UserNavigation { get; set; } = null!;
}
