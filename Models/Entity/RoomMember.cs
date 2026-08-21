using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vennAPIRemade.Models.Entity
{
    public class RoomMember : BaseEntity
    {
        public int RoomId { get; set; } // This is the foreign key for the room!
        public int UserId { get; set; } // This is the user joining the room
        public bool IsAccepted { get; set; } = false;

    }
}