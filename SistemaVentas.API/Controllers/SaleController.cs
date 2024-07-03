using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BILL.Services.Contract;
using SistemaVenta.DTO;
using SistemaVentas.API.Utility;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] SaleDTO sale)
        {
            var rsp = new Response<SaleDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _saleService.register(sale);

            }
            catch (Exception e)
            {
                rsp.status = false;
                rsp.message = e.Message;

            }
            return Ok(rsp);
        }

        [HttpGet]
        [Route("historial")]
        public async Task<IActionResult> Historial(string searchBy, string saleNumber, string initial_date, string final_date, string clientName)
        {
            var rsp = new Response<List<SaleDTO>>();

            if (saleNumber == null)
            {
                saleNumber = "";
            }
            if (initial_date == null)
            {
                initial_date = "";
            }
            if (final_date == null)
            {
                final_date = "";
            }
            if (clientName == null)
            {
                clientName = "";
            }

            try
            {
                rsp.status = true;
                rsp.value = await _saleService.historial(searchBy, saleNumber, initial_date, final_date, clientName);

            }
            catch (Exception e)
            {
                rsp.status = false;
                rsp.message = e.Message;

            }
            return Ok(rsp);
        }

        [HttpGet]
        [Route("report")]
        public async Task<IActionResult> Report(string initial_date, string final_date)
        {
            var rsp = new Response<List<ReportDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _saleService.report(initial_date, final_date);

            }
            catch (Exception e)
            {
                rsp.status = false;
                rsp.message = e.Message;

            }
            return Ok(rsp);
        }

       
    }


}
