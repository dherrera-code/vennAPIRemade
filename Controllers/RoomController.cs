using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Interface.IService;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("CreateRoom")]
        public async Task<ActionResult<RoomEntity>> CreateNewRoom(RoomDTO newRoom)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newRoom.Title) || string.IsNullOrWhiteSpace(newRoom.Category))
                {
                    return BadRequest(new { Message = "Room Title and Category is required" });
                }

                bool success = await _roomService.CreateNewRoom(newRoom);
                if (success) return Ok(success);
                return BadRequest(new { Message = $"Unable to create Room." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllRooms")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetAllRooms()
        {
            try
            {
                var roomsList = await _roomService.GetAllRooms();
                return Ok(roomsList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRoomById/{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoomById(int id)
        {
            try
            {
                RoomDTO room = await _roomService.GetRoomById(id);
                return Ok(room);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCreatedAndJoinedRoomsByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRelevantRoomsByUserId(int id)
        {
            var roomList = await _roomService.GetRelevantRoomsById(id);
            return Ok(roomList);
        }
    
        [HttpPut("UpdateRoom/{id}")]
        public async Task<ActionResult<RoomDTO>> UpdateRoom(int id, [FromBody] RoomDTO updatedRoom)
        {
            var result = await _roomService.UpdateRoom(id, updatedRoom);
            return Ok(result);
        }

        [HttpDelete("DeleteRoomById")]
        public async Task<ActionResult<bool>> RemoveRoom(int roomId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); //this is used to ensure the host of the room is the one deleting the room when endpoint is called!
            
            bool result = await _roomService.DeleteRoomById(roomId, userId);
            if(result) return Ok(true);
            return BadRequest(false);

        }
    }
}