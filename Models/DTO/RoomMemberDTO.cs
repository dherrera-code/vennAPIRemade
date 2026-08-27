using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Models.DTO
{
    public class RoomMemberDTO
    {
        public int RoomId { get; set; } // This is the foreign key for the room!
        public RoomDTO? Room { get; set; }
        public int MemberId { get; set; } // This is the user joining the room
        public bool IsAccepted { get; set; } = false;
        [ForeignKey("MemberId")]
        public UserDTO? MemberInfo { get; set; }
    }
}