using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmarBariAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController(IFlatsService flatsService) : ControllerBase
    {
        private readonly IFlatsService flatsService = flatsService;

        [HttpGet("GetAllFlats")]
        public async Task<IActionResult> GetAllFlats()
        {
            var result = await flatsService.GetAllFlats();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await flatsService.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetByHomeId/{id}")]
        public async Task<IActionResult> GetByHomeId(long id)
        {
            var result = await flatsService.GetByHomeId(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FlatRequestDto dto)
        {
            var data = await flatsService.Create(dto);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] FlatRequestDto dto)
        {
            var response = await flatsService.Update(dto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await flatsService.Delete(id);
            return Ok(response);
        }
    }
}
