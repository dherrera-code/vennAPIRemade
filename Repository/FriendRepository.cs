using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}