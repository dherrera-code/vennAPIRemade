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
    }
}