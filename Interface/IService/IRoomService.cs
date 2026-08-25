using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Interface.IService
{
    public interface IRoomService
    {
        Task<bool> CreateNewRoom(RoomDTO newRoom);
        Task<IEnumerable<RoomDTO>> GetAllRooms();
        Task<RoomDTO> GetRoomById(int id);
    }
}