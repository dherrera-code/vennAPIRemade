using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Models.DTO
{
    public class RoomMemberDTO
    {
        public int UserId { get; set; }
        public bool IsAccepted { get; set; }
        public UserDTO? MemberInfo { get; set; }
    }
}