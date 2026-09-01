using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Interface.IService;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _friendService;
        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpPost("SendFriendRequest/{requesterId}/{receiverId}")]
        public async Task<ActionResult<bool>> SendFriendRequest(int requesterId, int receiverId)
        {
            var success = await _friendService.SendFriendRequest(requesterId, receiverId);
            if(!success) return BadRequest(new{message = "Friend Request already created!"});
            return Ok(success);
        }

        [HttpGet("GetAllPendingFriends/{userId}")]
        public async Task<ActionResult<IEnumerable<FriendDTO>>> GetAllPendingFriends(int userId)
        {
            return Ok(await _friendService.GetPendingFriends(userId));
        }

        // [HttpGet("GetAllAcceptedFriends/{userId}")]
        // public async Task<ActionResult<IEnumerable<FriendDTO>>> GetAllAcceptedFriends(int userId)
        // {
        //     return Ok(await _friendService.GetAcceptedFriends(userId));
        // }

        // [HttpPut("AddFriendStatus/{requesterId}/{receiverId}")]
        // public async Task<ActionResult<bool>> UpdateFriendStatusToAccepted(int requesterId, int receiverId)
        // {
        //     FriendDTO result = await _friendService.UpdateStatusToAccepted(requesterId, receiverId);
        //     if(result is null) return BadRequest(false);

        //     return Ok(true);
        // }

        // [HttpPut("DenyFriendRequestByRequestId/{requesterId}/{receiverId}")]
        // public async Task<ActionResult<bool>> RemoveFriendStatusToDenied(int requesterId, int receiverId)
        // {
        //     bool success = await _friendService.RemoveFriendInvite(requesterId, receiverId);

        //     if(!success) return BadRequest(false);

        //     return Ok(success);
        // }
    }
}