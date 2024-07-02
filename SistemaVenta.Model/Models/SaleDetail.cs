using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class SaleDetail
{
    public int Id { get; set; }

    public int? SaleId { get; set; }

    public int? ProductId { get; set; }

    public int? ClientId { get; set; }

    public int? Cantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Sale? Sale { get; set; }
}
