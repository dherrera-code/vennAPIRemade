using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Interface;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        // Get User By Id

        [HttpPut("UpdateUserProfile")]
        public async Task<ActionResult<UserDTO>> UpdateUserInfo(UserDTO updatedUser)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Taken as a string!

            if (userId == null) return Unauthorized();
            try
            {
                UserDTO result = await _userService.UpdateUser(userId, updatedUser);

                if (result is null) return BadRequest(new { Message = "Unable to Update user" });

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}