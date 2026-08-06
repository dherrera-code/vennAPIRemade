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
                    return Ok(result);
                else 
                    return BadRequest("Unable to create account at this time.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO userLogin)
        {
            var success = await _userService.Login(userLogin);

            if(success is null)
            return Unauthorized(new {Message = "Login was unsuccessful"});

            return Ok(new {Token = success});
        }

        // Get User By Id

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }
    }
}