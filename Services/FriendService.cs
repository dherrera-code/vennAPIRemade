using AutoMapper;
using vennAPIRemade.Interface.IRepo;
using vennAPIRemade.Interface.IService;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _friendRepo;
        private readonly IMapper _mapper;
        public FriendService(IFriendRepository friendRespository, IMapper mapper)
        {
            _friendRepo = friendRespository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FriendDTO>> GetAcceptedFriends(int userId)
        {
            IEnumerable<Friend> acceptedFriends = await _friendRepo.GetAcceptedFriends(userId);
            return _mapper.Map<IEnumerable<FriendDTO>>(acceptedFriends);
        }

        public async Task<IEnumerable<FriendDTO>> GetPendingFriends(int userId)
        {
            IEnumerable<Friend> pendingList = await _friendRepo.GetPendingRequests(userId);
            return _mapper.Map<IEnumerable<FriendDTO>>(pendingList);
        }

        public async Task<bool> RemoveFriendInvite(int requesterId, int receiverId)
        {
            var friendEntry = await _friendRepo.GetFriendEntry(requesterId, receiverId);
            if(friendEntry is null) return false;

            return await _friendRepo.RemoveFriendRequest(friendEntry);
        }

        public async Task<bool> SendFriendRequest(int requesterId, int receiverId)
        {
            var friendEntry = await _friendRepo.GetFriendEntry(requesterId, receiverId);
            if (friendEntry != null) return false;
            
            friendEntry = await _friendRepo.GetFriendEntry(receiverId, requesterId);
                if (friendEntry != null)
                    return false;
            return await _friendRepo.CreatePendingFriendEntry(requesterId, receiverId);
        }

        public async Task<bool> UpdateStatusToAccepted(int requesterId, int receiverId)
        {
            var friendEntry = await _friendRepo.GetFriendEntry(requesterId, receiverId);
            if(friendEntry is null) return false;

            friendEntry.Status = 2;
            friendEntry.AcceptedAt = DateTime.UtcNow;
            return await _friendRepo.UpdateFriendStatus(friendEntry);
        }
    }
}