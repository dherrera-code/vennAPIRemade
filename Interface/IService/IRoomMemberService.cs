using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Interface.IService
{
    public interface IRoomMemberService
    {
        Task<bool> ChangeMemberStatusToAccepted(RoomMemberDTO roomMemberDTO);
        Task<IEnumerable<RoomMemberDTO>> GetAllJoinedMembersByRoom(int roomId);
        Task<IEnumerable<PendingInvitationDTO>> GetPendingInvitesByUserId(int userId);
        Task<bool> InviteRoomMember(RoomMemberDTO newRoomMember);
        Task<bool> RemoveMemberFromRoom(RoomMemberDTO memberToRemove);
    }
}