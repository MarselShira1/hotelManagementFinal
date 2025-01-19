using System;
using System.Collections.Generic;

namespace hotelManagement.DAL.Persistence.Entities;

public partial class Pagese : BaseEntity<int>
{
    public int Id { get; set; }

    public int Fature { get; set; }

    public decimal Total { get; set; }

    public string Menyre { get; set; } = null!;

    public DateOnly DatePagese { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual Fature FatureNavigation { get; set; } = null!;
}
