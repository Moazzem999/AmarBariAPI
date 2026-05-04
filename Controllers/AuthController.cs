using AmarBariAPI.Dtos.User;
using AmarBariAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmarBariAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var data = await userService.Login(dto);
            return Ok(data);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] UserRequestDto dto)
        {
            var data = await userService.Create(dto);
            return Ok(data);
        }
    }
}
