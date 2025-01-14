using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Privilegj
{
    public int Id { get; set; }

    public int Action { get; set; }

    public int Rol { get; set; }

    public virtual Action ActionNavigation { get; set; } = null!;

    public virtual Role RolNavigation { get; set; } = null!;
}
