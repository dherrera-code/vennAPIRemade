using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vennAPIRemade.Models.DTO
{
    public class UserAvailabilityDTO
    {
        public int UserId { get; set; }
        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }
        public int StatusId { get; set; }
    }
}