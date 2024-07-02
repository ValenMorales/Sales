using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BILL.Services.Contract
{
    public interface ISaleService
    {
        Task<SaleDTO> register(SaleDTO sale);
        Task<List<SaleDTO>> historial(string searchBy, string saleNumber, string initialDate, string finalDate, string clientName);

        Task<List<ReportDTO>> report(string initial_date, string final_date);
    }
}
