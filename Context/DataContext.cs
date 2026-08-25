using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoomEntity> Room { get; set; }
        public DbSet<RoomMember> RoomMembers { get; set; }
        public DbSet<Friend> Friends { get; set; }  
    
    }
}