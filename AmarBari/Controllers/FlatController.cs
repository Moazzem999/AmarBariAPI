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
        private readonly IFlatServices _flatServices;

        public FlatController(IFlatServices flatServices)
        {
            _flatServices = flatServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlats()
        {
            try
            {
                return Ok(await _flatServices.GetFlats());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
