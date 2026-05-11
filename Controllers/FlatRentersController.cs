using AmarBariAPI.Dtos.Home;
using AmarBariAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmarBariAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FlatRentersController(IFlatRenterService flatRenterService) : ControllerBase
    {
        private readonly IFlatRenterService flatRenterService = flatRenterService;

        [HttpGet("GetAllFlatRenters")]
        public async Task<IActionResult> GetAllFlatRenters()
        {
            var result = await flatRenterService.GetAllFlatRenters();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await flatRenterService.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetByFlatId/{id}")]
        public async Task<IActionResult> GetByFlatId(long id)
        {
            var result = await flatRenterService.GetByFlatId(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FlatRenterRequestDto dto)
        {
            var data = await flatRenterService.Create(dto);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] FlatRenterRequestDto dto)
        {
            var response = await flatRenterService.Update(dto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await flatRenterService.Delete(id);
            return Ok(response);
        }
    }
}
