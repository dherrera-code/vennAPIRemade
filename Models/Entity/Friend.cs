using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vennAPIRemade.Models.Entity
{
    public class Friend: BaseEntity
    {
        public int RequestedId { get; set; }
        public int ReceiverId { get; set; }
        public int Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }    
    }
}