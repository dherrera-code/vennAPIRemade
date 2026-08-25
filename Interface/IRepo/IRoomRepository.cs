using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Interface.IRepo
{
    public interface IRoomRepository
    {
        Task<bool> AddNewRoom(RoomEntity newRoom);
        Task<List<RoomEntity>> GetAllRooms();
        Task<RoomEntity> GetRoomById(int id);
    }
}