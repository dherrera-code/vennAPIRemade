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
            return await _dbContext.Room.ToListAsync();
        }

        public async Task<RoomEntity> GetRoomById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Room.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}