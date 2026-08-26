using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Interface.IService
{
    public interface IRoomService
    {
        Task<bool> CreateNewRoom(RoomDTO newRoom);
        Task<bool> DeleteRoomById(int roomId, int userId);
        Task<IEnumerable<RoomDTO>> GetAllRooms();
        Task<IEnumerable<RoomDTO>> GetRelevantRoomsById(int id);
        Task<RoomDTO> GetRoomById(int id);
        Task<RoomDTO> UpdateRoom(int id, RoomDTO updatedRoom);
    }
}