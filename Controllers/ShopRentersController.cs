using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmarBariAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopRentersController(IShopRenterService shopRenterService) : ControllerBase
    {
        private readonly IShopRenterService shopRenterService = shopRenterService;

        [HttpGet("GetAllShopRenters")]
        public async Task<IActionResult> GetAllShopRenters()
        {
            var result = await shopRenterService.GetAllShopRenters();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ShopRenterRequestDto dto)
        {
            var data = await shopRenterService.Create(dto);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] ShopRenterRequestDto dto)
        {
            var response = await shopRenterService.Update(dto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await shopRenterService.Delete(id);
            return Ok(response);
        }
    }
}
