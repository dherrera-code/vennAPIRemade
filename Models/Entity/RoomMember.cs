using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vennAPIRemade.Models.Entity
{
    public class RoomMember : BaseEntity
    {
        public int RoomId { get; set; } // This is the foreign key for the room!
        public RoomEntity Room { get; set; }
        public int MemberId { get; set; } // This is the user joining the room
        public bool IsAccepted { get; set; } = false;
        [ForeignKey("MemberId")]
        public UserEntity? MemberInfo { get; set; }


    }
}