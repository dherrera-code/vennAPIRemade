using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace vennAPIRemade.Models.Entity
{
    public class UserAvailability 
    {
        public int Id { get; set; }

        [ForeignKey("UserEntity")]
        public int UserId { get; set; }
        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }
        [ForeignKey("AvailabilityStatus")]
        public int StatusId { get; set; }
    }
}