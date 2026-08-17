using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vennAPIRemade.Models.Entity
{
    public class RoomEntity: BaseEntity
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public DateOnly EventDate { get; set; }
        public TimeOnly? ChosenHour { get; set; }
        public bool IsRoomActive { get; set; } = true;
        public int UserId { get; set; }
        // Add Collection of Users who are members
    }
}