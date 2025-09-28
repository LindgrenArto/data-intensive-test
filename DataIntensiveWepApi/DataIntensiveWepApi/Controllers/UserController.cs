using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataIntensiveWepApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{db}")]
        public IActionResult GetUsers([FromRoute] int db)
        {
            try
            {
                if (!Enum.IsDefined(typeof(DataStore), db))
                    return BadRequest("Unknown database.");

                var store = (DataStore)db;
                List<UserDTO> users = _userService.GetUsers(store);
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }


        [HttpPut("{db}")]
        public IActionResult UpdateUser([FromRoute] int db, [FromBody] UserDTO user)
        {
            try
            {
                if (!Enum.IsDefined(typeof(DataStore), db))
                    return BadRequest("Unknown database.");

                var store = (DataStore)db;

                UserDTO existing = _userService.GetUserByUuid(store, user.UserUuid);

                if (existing == null)
                    return NotFound("User is not found.");

                UserDTO updatedUser = _userService.UpdateUser(store, user);

                return Ok(updatedUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}