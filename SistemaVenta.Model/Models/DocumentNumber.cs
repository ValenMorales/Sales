using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class DocumentNumber
{
    public int Id { get; set; }

    public int LastNumber { get; set; }

    public DateTime? RegisterDate { get; set; }
}
