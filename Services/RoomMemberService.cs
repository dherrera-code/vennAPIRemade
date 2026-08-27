using AutoMapper;
using vennAPIRemade.Interface.IRepo;
using vennAPIRemade.Interface.IService;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Services
{
    public class RoomMemberService : IRoomMemberService
    {
        private readonly IRoomMemberRepository _roomMemberRepo;
        private readonly IMapper _mapper;
        public RoomMemberService(IRoomMemberRepository roomMemberRepository, IMapper mapper)
        {
            _roomMemberRepo = roomMemberRepository;
            _mapper = mapper;
        }

        public async Task<bool> ChangeMemberStatusToAccepted(RoomMemberDTO roomMemberDTO)
        {
            RoomMember member = await _roomMemberRepo.GetInviteInstance(roomMemberDTO.RoomId, roomMemberDTO.MemberId);
            if(member is null) return false;
            member.IsAccepted = true;
            return await _roomMemberRepo.UpdateStatus(member);
        }

        public async Task<IEnumerable<RoomMemberDTO>> GetAllJoinedMembersByRoom(int roomId)
        {
            var memberList = await _roomMemberRepo.GetAllAcceptedByRoom(roomId);
            return _mapper.Map<IEnumerable<RoomMemberDTO>>(memberList);
        }

        public async Task<IEnumerable<PendingInvitationDTO>> GetPendingInvitesByUserId(int userId)
        {
            var pendingInviteList = await _roomMemberRepo.GetAllPendingInvites(userId);
            return 
            // _mapper.Map<IEnumerable<RoomMemberDTO>>
            pendingInviteList;
        }

        public async Task<bool> InviteRoomMember(RoomMemberDTO newRoomMember)
        {
            var invite = await _roomMemberRepo.GetInviteInstance(newRoomMember.RoomId, newRoomMember.MemberId);

            if(invite != null) return false;

            return await _roomMemberRepo.AddNewInvite(newRoomMember);
        }

        public async Task<bool> RemoveMemberFromRoom(RoomMemberDTO memberToRemove)
        {
            RoomMember member = await _roomMemberRepo.GetInviteInstance(memberToRemove.RoomId, memberToRemove.MemberId);
            if(member is null) return false;
            member.IsDeleted = true;
            return await _roomMemberRepo.UpdateStatus(member);
        }
    }
}