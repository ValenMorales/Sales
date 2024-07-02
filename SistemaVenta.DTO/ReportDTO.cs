using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ReportDTO
    {
        public string? DocumentNumber { get; set; }
        public string? PayMethod { get; set; }
        public string? RegisterDate { get; set; }
        public string? Product { get; set; }
        public string? TotalSale { get; set; }
        public int? Cantity { get; set; }
        public string? Price { get; set; }
        public string? Total { get; set; }
    }
}
