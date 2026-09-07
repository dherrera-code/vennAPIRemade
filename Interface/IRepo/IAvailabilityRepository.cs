using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Interface.IRepo
{
    public interface IAvailabilityRepository
    {
        Task AddAsync(UserAvailability newEntity);
        Task<IEnumerable<UserAvailability>> GetAvailability(int userId);
        Task<IEnumerable<UserAvailability>> GetAvailabilityByDay(DayOfWeek dayOfWeek, int userId);
        Task<IEnumerable<UserAvailability>> GetWeeklyAvailability(int userId);
        Task RemoveAsync(UserAvailability entity);
        Task<bool> SaveAsync();
    }
}