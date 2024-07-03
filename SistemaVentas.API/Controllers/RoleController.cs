using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BILL.Services.Contract;
using SistemaVenta.DTO;
using SistemaVentas.API.Utility;
namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IRolService _rolService;

        public RoleController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var rsp = new Response<List<RolDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _rolService.List();

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
