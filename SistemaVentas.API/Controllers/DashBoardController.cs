using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BILL.Services.Contract;
using SistemaVenta.DTO;
using SistemaVentas.API.Utility;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashboardService _dashBoardService;

        public DashBoardController(IDashboardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var rsp = new Response<DashBoardDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _dashBoardService.Summary();

            }
            catch(Exception e)
            {
                rsp.status = false;
                rsp.message = e.Message;

            }
            return Ok(rsp);
        }
    }
}
