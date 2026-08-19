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
        [HttpGet("GetById")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user is null)
                return NotFound("User Id Doesn't Exist.");
            return Ok(user);
        }

        [HttpGet("GetUserByUsername")]
        public async Task<ActionResult<UserDTO>> GetUserByUsername(string username)
        {
            UserDTO user = await _userService.GetUserByName(username);
            if(user is null) return BadRequest("Username Does Not Exist");
            return Ok(user);
        }

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

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<bool>> DeleteUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            try
            {
                var success = await _userService.DeleteUser(int.Parse(userId));

                return success;
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message});
            }

        }
    }
}