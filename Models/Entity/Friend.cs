using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vennAPIRemade.Models.Entity
{
    public class Friend: BaseEntity
    {
        public int RequesterId { get; set; }
        public int ReceiverId { get; set; }
        public int Status { get; set; }
        // Pending = 1, Accepted = 2, Blocked = 3
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }    
        public UserEntity? Requester { get; set; }
    }
}