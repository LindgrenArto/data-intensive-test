using DataIntensiveWepApi.DTOModels;
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
                List<UserDTO> users = _userService.GetUsers(db);
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}