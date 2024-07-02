using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class SaleDTO
    {
        public int Id { get; set; }

        public string? DocumentNumber { get; set; }

        public string? PayMethod { get; set; }

        public string? TextTotal { get; set; }

        public string? RegisterDate { get; set; }

        public virtual ICollection<SaleDetailDTO> SaleDetails { get; set; } 
    }
}
