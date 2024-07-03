using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BILL.Services.Contract;
using SistemaVenta.DTO;
using SistemaVentas.API.Utility;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var rsp = new Response<List<UserDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _userService.list();

            }
            catch(Exception e)
            {
                rsp.status = false;
                rsp.message = e.Message;

            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var rsp = new Response<SessionDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _userService.validateCredentials(login.Email, login.Password);

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
        public async Task<IActionResult> Save([FromBody] UserDTO user)
        {
            var rsp = new Response<UserDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _userService.create(user);

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
        public async Task<IActionResult> Update([FromBody] UserDTO user)
        {
            var rsp = new Response<UserDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _userService.update(user);

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
                rsp.value = await _userService.delete(id);
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
