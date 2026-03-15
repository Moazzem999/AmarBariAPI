using AmarBariAPI.Dtos.User;
using AmarBariAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmarBariAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUsersRepository usersRepository) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var data = await usersRepository.Login(dto);
            return Ok(data);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] UserRequestDto dto)
        {
            var data = await usersRepository.Create(dto);
            return Ok(data);
        }
    }
}
