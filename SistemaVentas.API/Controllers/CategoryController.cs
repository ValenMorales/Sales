using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BILL.Services.Contract;
using SistemaVenta.DTO;
using SistemaVentas.API.Utility;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var rsp = new Response<List<CategoryDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _categoryService.List();

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
