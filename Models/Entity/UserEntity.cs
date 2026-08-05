using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vennAPIRemade.Models.Entity
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public string Description { get; set; }
        public string UserIcon { get; set; }
        public DateTime AccountCreated { get; set; } = DateTime.UtcNow;
        
    }
}