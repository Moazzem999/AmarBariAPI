using AmarBari.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmarBari.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        private readonly IFlatServices _causesServices;

        public FlatController(IFlatServices causesServices)
        {
            _causesServices = causesServices;
        }

        [HttpGet("GetAllFlats")]
        public async Task<IActionResult> GetAllCauses()
        {
            var data = await _causesServices.GetAllFlats();
            return Ok(data);
        }
    }
}
