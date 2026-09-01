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
    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _dbContext;
        public RoomRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddNewRoom(RoomEntity newRoom)
        {
            await _dbContext.Room.AddAsync(newRoom);
            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<List<RoomEntity>> GetAllRooms()
        {
            return await _dbContext.Room.Where(room => !room.IsDeleted).AsNoTracking().ToListAsync();
        }

        public async Task<List<RoomEntity>> GetRelevantRoomsByUserId(int id)
        {
            return await _dbContext.Room.AsNoTracking()
            .Where(room => room.UserId == id && !room.IsDeleted  || room.Members.Any(m => m.MemberId == id && m.IsAccepted && !m.IsDeleted))
            .Include(joined => joined.Members)
            .ToListAsync();
        }

        public async Task<RoomEntity> GetRoomById(int id)
        {
            return await _dbContext.Room.Include(mem => mem.Members.Where(memb => memb.IsAccepted && !memb.IsDeleted)).ThenInclude(info => info.MemberInfo)
            .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> UpdateRoomDetails(RoomEntity currentRoom)
        {
            _dbContext.Room.Update(currentRoom);
            return await _dbContext.SaveChangesAsync() != 0;
        }
    }
}