using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Interface.IRepo
{
    public interface IFriendRepository
    {
        Task<bool> CreatePendingFriendEntry(int requesterId, int receiverId);
        Task<Friend> GetFriendEntry(int requesterId, int receiverId);
        Task<IEnumerable<Friend>> GetPendingRequests(int userId);
    }
}