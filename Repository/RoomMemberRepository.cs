using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using vennAPIRemade.Context;
using vennAPIRemade.Interface.IRepo;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Repository
{
    public class RoomMemberRepository : IRoomMemberRepository
    {
        private readonly DataContext _dbContext;
        public RoomMemberRepository(DataContext dataContext)
        {
            _dbContext = dataContext;
        }
        public async Task<bool> AddNewInvite(RoomMemberDTO newRoomMember)
        {
            RoomMember inviteNewMember = new()
            {
              RoomId = newRoomMember.RoomId,
              IsAccepted = false,
              MemberId = newRoomMember.MemberId,  
            };
            await _dbContext.RoomMembers.AddAsync(inviteNewMember);
            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<IEnumerable<RoomMember>> GetAllAcceptedByRoom(int roomId)
        {
            return await _dbContext.RoomMembers.Where(mem => mem.RoomId == roomId && mem.IsAccepted).Include(mem => mem.MemberInfo)
            .ToListAsync();
        }

        public async Task<IEnumerable<PendingInvitationDTO>> GetAllPendingInvites(int userId)
        {
            var pendingInviteList = await _dbContext.RoomMembers.Where(i => i.MemberId == userId && !i.IsAccepted && !i.IsDeleted)
            .Select(r => new PendingInvitationDTO
            {
                RoomId = r.Room.Id,
                Title = r.Room.Title,
                Category = r.Room.Category,
                EventDate = r.Room.EventDate,
                RequesterId = r.Room.UserId,
                RequesterName = r.Room.User.Username,
                RequesterIcon = r.Room.User.UserIcon
            })
            .ToListAsync();
            return pendingInviteList;
        }

        public async Task<RoomMember> GetInviteInstance(int roomId, int memberId)
        {
            return await _dbContext.RoomMembers.SingleOrDefaultAsync(invite => invite.RoomId == roomId && invite.MemberId == memberId && !invite.IsDeleted);
        }

        public async Task<bool> UpdateStatus(RoomMember member)
        {
            _dbContext.RoomMembers.Update(member);
            return await _dbContext.SaveChangesAsync() != 0;
        }
    }
}