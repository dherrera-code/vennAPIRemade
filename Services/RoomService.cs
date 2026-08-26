using AutoMapper;
using vennAPIRemade.Interface.IRepo;
using vennAPIRemade.Interface.IService;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepo;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository roomRepository, IMapper mapper)  
        {
            _roomRepo = roomRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateNewRoom(RoomDTO newRoom)
        {
            RoomEntity room = _mapper.Map<RoomEntity>(newRoom);
            return await _roomRepo.AddNewRoom(room);
        }

        public async Task<IEnumerable<RoomDTO>> GetAllRooms()
        {
            var roomList = await _roomRepo.GetAllRooms();
            return _mapper.Map<List<RoomDTO>>(roomList);
        }

        public async Task<RoomDTO> GetRoomById(int id)
        {
            var room = await _roomRepo.GetRoomById(id);
            if(room is null) throw new Exception( $"Room with id: {id} doesn't exist.");
            return _mapper.Map<RoomDTO>(room);
        }

        public async Task<RoomDTO> UpdateRoom(int id, RoomDTO updatedRoom)
        {
            RoomEntity currentRoom = await _roomRepo.GetRoomById(id);
            if(currentRoom is null) throw new Exception($"Unable to find room to update with id {id}.");

            currentRoom.Title = updatedRoom.Title;
            currentRoom.Category = updatedRoom.Category;
            currentRoom.EventDate = updatedRoom.EventDate;
            currentRoom.GoldenHour = updatedRoom.GoldenHour;

            bool result = await _roomRepo.UpdateRoomDetails(currentRoom);
            if(result) return _mapper.Map<RoomDTO>(currentRoom);
            throw new Exception("Unable to update room at this time. Please try again later.");

        }

        public async Task<bool> DeleteRoomById(int roomId, int userId)
        {
            RoomEntity roomToDelete = await _roomRepo.GetRoomById(roomId);

            if(roomToDelete is null) throw new Exception($"Unable to find room to remove with id {roomId}.");

            if(roomToDelete.UserId != userId) throw new Exception("User currently logged in is unauthorized to remove room");

            roomToDelete.IsDeleted = true;
            return await _roomRepo.UpdateRoomDetails(roomToDelete);
        }

        public async Task<IEnumerable<RoomDTO>> GetRelevantRoomsById(int id)
        {
            var relevantRooms = await _roomRepo.GetRelevantRoomsByUserId(id);
            return _mapper.Map<List<RoomDTO>>(relevantRooms);
        }
    }
}