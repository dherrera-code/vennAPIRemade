using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vennAPIRemade.Models.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public DateTime? AccountCreated { get; set; }
        public string? UserIcon { get; set; }
        
    }
}