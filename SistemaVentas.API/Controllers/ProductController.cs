using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BILL.Services.Contract;
using SistemaVenta.DTO;
using SistemaVentas.API.Utility;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var rsp = new Response<List<ProductDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _productService.list();

            }
            catch(Exception e)
            {
                rsp.status = false;
                rsp.message = e.Message;

            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO product)
        {
            var rsp = new Response<ProductDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _productService.create(product);

            }
            catch(Exception e)
            {
                rsp.status = false;
                rsp.message = e.Message;

            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("update")]

        public async Task<IActionResult> update([FromBody] ProductDTO product)
        {
            var rsp = new Response<ProductDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _productService.update(product);

            }
            catch(Exception e)
            {
                rsp.status = false;
                rsp.message = e.Message;

            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.status = true;
                rsp.value = await _productService.delete(id);

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
