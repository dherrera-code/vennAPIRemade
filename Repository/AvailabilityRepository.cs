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
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly DataContext _dbContext;
        public AvailabilityRepository(DataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public async Task AddAsync(UserAvailability newEntity)
        {
            await _dbContext.UserAvailability.AddAsync(newEntity);
        }

        public async Task<IEnumerable<UserAvailability>> GetAvailability(int userId)
        {
            return await _dbContext.UserAvailability
            .Where(a => a.UserId == userId)
            .ToListAsync();
        }

        public async Task<IEnumerable<UserAvailability>> GetAvailabilityByDay(DayOfWeek dayOfWeek, int userId)
        {
            return await _dbContext.UserAvailability
            .AsNoTracking()
            .Where(a => a.UserId == userId && a.Day == dayOfWeek)
            .ToListAsync();
        }

        public async Task<IEnumerable<UserAvailability>> GetWeeklyAvailability(int userId)
        {
            return await _dbContext.UserAvailability
            .AsNoTracking()
            .Where(u => u.UserId == userId)
            .ToListAsync();
        }

        public async Task RemoveAsync(UserAvailability entity)
        {
            _dbContext.UserAvailability.Remove(entity);
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() != 0;
        }
    }
}