using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class SaleDetailDTO
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }
        public string? ProductDescription { get; set; }

        public int? ClientId { get; set; }

        public string? ClientName { get; set; }

        public int? Cantity { get; set; }

        public string? TextPrice { get; set; }

        public string? TextTotal { get; set; }
    }
}
