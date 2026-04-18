using AmarBariAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmarBariAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController(IShopsRepository shopsRepository) : ControllerBase
    {
        private readonly IShopsRepository shopsRepository = shopsRepository;

        [HttpGet("GetAllShops")]
        public async Task<IActionResult> GetAllShops()
        {
            var result = await shopsRepository.GetAllShops();
            return Ok(result);
        }
    }
}
