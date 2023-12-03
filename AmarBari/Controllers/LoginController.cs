using AmarBari.Dto;
using AmarBari.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmarBari.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly AmarBariDbContext _context;

        public LoginController(IConfiguration config, AmarBariDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginDto loginDto)
        {
            if (loginDto != null && loginDto.LoginId != null && loginDto.Password != null)
            {
                var user = await GetUser(loginDto.LoginId, loginDto.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("LoginId", user.LoginId),
                        new Claim("Name", user.Name),
                        new Claim("UserType", user.UserType)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signIn);

                    string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                    var expiration = DateTime.Now.AddMinutes(30);
                    return Ok(new { AccessToken = accessToken, Expiration = expiration });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUser(string loginId, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.LoginId == loginId && u.Password == password && u.IsActive == true);
        }
    }
}
