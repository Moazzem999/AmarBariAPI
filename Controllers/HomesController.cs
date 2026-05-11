using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmarBariAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController(IHomesService homesService) : ControllerBase
    {
        private readonly IHomesService homesService = homesService;

        [HttpGet("GetAllHomes")]
        public async Task<IActionResult> GetAllHomes()
        {
            var result = await homesService.GetAllHomes();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await homesService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HomeRequestDto dto)
        {
            var data = await homesService.Create(dto);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] HomeRequestDto dto)
        {
            var response = await homesService.Update(dto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await homesService.Delete(id);
            return Ok(response);
        }
    }
}
