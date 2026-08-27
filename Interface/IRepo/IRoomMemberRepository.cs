using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Interface.IRepo
{
    public interface IRoomMemberRepository
    {
        Task<bool> AddNewInvite(RoomMemberDTO newRoomMember);
        Task<IEnumerable<RoomMember>> GetAllAcceptedByRoom(int roomId);
        Task<IEnumerable<PendingInvitationDTO>> GetAllPendingInvites(int userId);
        Task<RoomMember> GetInviteInstance(int roomId, int memberId);
        Task<bool> UpdateStatus(RoomMember member);
    }
}