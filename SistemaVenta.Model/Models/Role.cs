using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
