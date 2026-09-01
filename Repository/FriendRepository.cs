using Microsoft.EntityFrameworkCore;
using vennAPIRemade.Context;
using vennAPIRemade.Interface.IRepo;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Repository
{
    public class FriendRepository : IFriendRepository
    {
        private readonly DataContext _dbContext;
        public FriendRepository(DataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public async Task<bool> CreatePendingFriendEntry(int requesterId, int receiverId)
        {
            Friend newFriend = new()
            {
                RequesterId = requesterId,
                ReceiverId = receiverId,
                RequestedAt = DateTime.UtcNow,
                Status = 1
            };
            await _dbContext.Friends.AddAsync(newFriend);
            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<IEnumerable<Friend>> GetAcceptedFriends(int userId)
        {
            return await _dbContext.Friends.Where(f => (f.RequesterId == userId || f.ReceiverId == userId) && f.Status == 2)
            .Include(friendInfo => friendInfo.Requester)
            .Include(receiverInfo => receiverInfo.Receiver)
            .ToListAsync();
        }

        public async Task<Friend> GetFriendEntry(int requesterId, int receiverId)
        {
            return await _dbContext.Friends
            .SingleOrDefaultAsync(e => e.RequesterId == requesterId && e.ReceiverId == receiverId || e.RequesterId == receiverId && e.ReceiverId == requesterId);
        }

        public async Task<IEnumerable<Friend>> GetPendingRequests(int userId)
        {
            return await _dbContext.Friends
            .Where(e => e.ReceiverId == userId && e.Status == 1)
            .Include(requesterInfo => requesterInfo.Requester)
            .ToListAsync();
        }

        public async Task<bool> RemoveFriendRequest(Friend friendEntry)
        {
            _dbContext.Remove(friendEntry);
            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> UpdateFriendStatus(Friend friendEntry)
        {
            _dbContext.Friends.Update(friendEntry);
            return await _dbContext.SaveChangesAsync() != 0;
        }
    }
}