using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Interface.IService
{
    public interface IFriendService
    {
        Task<IEnumerable<FriendDTO>> GetAcceptedFriends(int userId);
        Task<IEnumerable<FriendDTO>> GetPendingFriends(int userId);
        Task<bool> RemoveFriendInvite(int requesterId, int receiverId);
        Task<bool> SendFriendRequest(int requesterId, int receiverId);
        Task<bool> UpdateStatusToAccepted(int requesterId, int receiverId);
        // Task<FriendDTO> UpdateStatusToAccepted(int requesterId, int receiverId);
    }
}