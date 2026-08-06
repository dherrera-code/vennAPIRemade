using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Interface;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] NewUserDTO newUser)
        {
            try
            {
                var result = await _userService.CreateUser(newUser);
                if (result != null)
                    return Ok(new { newUser = result });
                else 
                    return BadRequest("Unable to create account at this time.");
            }
            catch (InvalidDataException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }
    }
}