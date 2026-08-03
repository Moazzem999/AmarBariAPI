using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmarBariAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController(IShopService shopService) : ControllerBase
    {
        [HttpGet("GetAllShops")]
        public async Task<IActionResult> GetAllShops()
        {
            var result = await shopService.GetAllShops();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await shopService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShopRequestDto dto)
        {
            var data = await shopService.Create(dto);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ShopRequestDto dto)
        {
            var response = await shopService.Update(dto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await shopService.Delete(id);
            return Ok(response);
        }
    }
}
