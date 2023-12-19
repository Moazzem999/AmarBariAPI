using AmarBari.Dto;
using AmarBari.Entities;
using AmarBari.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AmarBari.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserController(IUserServices userServices, IWebHostEnvironment webHostEnvironment)
        {
            this.userServices = userServices;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await userServices.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUser(long id)
        {
            try
            {
                var data = await userServices.GetUser(id);
                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] UserRequestDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                else if (String.IsNullOrEmpty(user.Name))
                {
                    ModelState.AddModelError("Name", "The Name field is required.");
                    return BadRequest(ModelState);
                }
                else if (String.IsNullOrEmpty(user.NidNo))
                {
                    ModelState.AddModelError("NidNo", "The NidNo field is required.");
                    return BadRequest(ModelState);
                }
                else if (user.Image == null)
                {
                    ModelState.AddModelError("Image", "The Image field is required.");
                    return BadRequest(ModelState);
                }
                else if (user.NidImage == null)
                {
                    ModelState.AddModelError("NidImage", "The NidImage field is required.");
                    return BadRequest(ModelState);
                }
                else if (user.Password == null)
                {
                    ModelState.AddModelError("Password", "The Password field is required.");
                    return BadRequest(ModelState);
                }

                var data = await userServices.AddUser(user);
                return CreatedAtAction(nameof(GetUser), new { id = data.Id }, data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error inserting data to the database");
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateUser(long id, [FromForm] UserRequestDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                else if (id != user.Id)
                {
                    return BadRequest("User id mismatch");
                }
                else if (String.IsNullOrEmpty(user.Name))
                {
                    ModelState.AddModelError("Name", "The Name field is required.");
                    return BadRequest(ModelState);
                }
                else if (String.IsNullOrEmpty(user.NidNo))
                {
                    ModelState.AddModelError("NidNo", "The NidNo field is required.");
                    return BadRequest(ModelState);
                }

                var userToUpdate = await userServices.GetUser(id);
                if (userToUpdate == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }

                return Ok(await userServices.UpdateUser(user));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                var userToDelete = await userServices.GetUser(id);
                if (userToDelete == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }

                var isDeleted = await userServices.DeleteUser(id);
                if (!isDeleted)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data from the database");
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data from the database");
            }
        }
    }
}
