using System;
using System.Collections.Generic;

using hotelManagement.DAL.Persistence.Entities;

namespace hotelManagament.DAL.Persistence.Entities;

public partial class Review : BaseEntity<int>
{
    public int Id { get; set; }

    public int User { get; set; }

    public int? Rating { get; set; }

    public string? Pershkrim { get; set; }

    public DateOnly Date { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual User UserNavigation { get; set; } = null!;
}
