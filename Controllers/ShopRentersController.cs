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

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await shopRenterService.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetByShopId/{id}")]
        public async Task<IActionResult> GetByShopId(long id)
        {
            var result = await shopRenterService.GetByShopId(id);
            return Ok(result);
        }

        [HttpGet("GetAllMaritalStatus")]
        public async Task<IActionResult> GetAllMaritalStatus()
        {
            var result = await shopRenterService.GetAllMaritalStatus();
            return Ok(result);
        }

        [HttpGet("GetAllReligion")]
        public async Task<IActionResult> GetAllReligion()
        {
            var result = await shopRenterService.GetAllReligion();
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
