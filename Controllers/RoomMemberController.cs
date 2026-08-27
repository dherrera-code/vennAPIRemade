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
    public class RoomMemberController : ControllerBase
    {
        private readonly IRoomMemberService _roomMemberService;
        public RoomMemberController(IRoomMemberService roomMemberService)
        {
            _roomMemberService = roomMemberService;
        }

        [HttpPost("InviteMemberToRoom")]
        public async Task<ActionResult<bool>> InviteMemberToRoom(RoomMemberDTO newRoomMember)
        {
            var result = await _roomMemberService.InviteRoomMember(newRoomMember);

            if (!result) return BadRequest(false);

            return Ok(true);
        }

        [HttpGet("GetAllMembersByRoomId/{roomId}")]
        public async Task<ActionResult<IEnumerable<RoomMemberDTO>>> GetAllMembersByRoomId(int roomId)
        {
            return Ok(await _roomMemberService.GetAllJoinedMembersByRoom(roomId));
        }

        [HttpGet("GetUserInvitationByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<PendingInvitationDTO>>> GetInvitesByUser(int userId)
        {
            var inviteList = await _roomMemberService.GetPendingInvitesByUserId(userId);

            if (inviteList is null) return NotFound();

            return Ok(inviteList);
        }

        [HttpPut("ChangeMemberStatusToAccepted")]
        public async Task<ActionResult<bool>> UpdateMemberStatusToAccepted(RoomMemberDTO roomMemberDTO)
        {
            bool result = await _roomMemberService.ChangeMemberStatusToAccepted(roomMemberDTO);

            if (!result) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("RemoveMemberFromRoom")]
        public async Task<ActionResult<bool>> RemoveMemberFromRoom(RoomMemberDTO memberToRemove)
        {
            var result = await _roomMemberService.RemoveMemberFromRoom(memberToRemove);
            if(!result) return BadRequest(result);

            return Ok(result);
        }
    }
}