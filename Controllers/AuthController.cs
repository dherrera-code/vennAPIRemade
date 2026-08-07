using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Interface;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] NewUserDTO newUser)
        {
            try
            {
                var result = await _authService.CreateUser(newUser);
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
            var success = await _authService.Login(userLogin);

            if(success is null)
            return Unauthorized(new {Message = "Login was unsuccessful"});

            return Ok(new {Token = success});
        }
    }
}