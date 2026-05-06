using AmarBariAPI.Dtos.Shop;
using AmarBariAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmarBariAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController(IContractsService contractsService) : ControllerBase
    {
        private readonly IContractsService contractsService = contractsService;

        [HttpGet("GetAllContracts")]
        public async Task<IActionResult> GetAllContracts()
        {
            var result = await contractsService.GetAllContracts();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await contractsService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ContractRequestDto dto)
        {
            var data = await contractsService.Create(dto);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] ContractRequestDto dto)
        {
            var response = await contractsService.Update(dto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await contractsService.Delete(id);
            return Ok(response);
        }
    }
}
