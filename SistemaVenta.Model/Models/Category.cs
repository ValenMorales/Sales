using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
