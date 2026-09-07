using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Interface.IService
{
    public interface IAvailabilityService
    {
        Task<IEnumerable<UserAvailabilityDTO>> AddNewAvailability(int userId, UserAvailabilityDTO[] availability);
        Task<IEnumerable<UserAvailabilityDTO>> GetDailyAvailabilityById(DayOfWeek dayOfWeek, int userId);
        Task<IEnumerable<UserAvailabilityDTO>> GetWeeklyAvailabilityByUserIdAsync(int userId);
    }
}