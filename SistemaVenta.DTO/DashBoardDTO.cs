using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class DashBoardDTO
    {
        public int TotalSales { get; set; }
        public string? TotalRevenue { get; set; }
        public List<WeekSalesDTO> LastWeekSales { get; set; }
    }
}
