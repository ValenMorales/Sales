using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class RoleMenu
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public int? IdMenu { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }

    public virtual Role? Role { get; set; }
}
