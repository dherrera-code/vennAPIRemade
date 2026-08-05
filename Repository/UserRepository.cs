using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vennAPIRemade.Interface;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;
using vennAPIRemade.Context;

namespace vennAPIRemade.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public UserRepository(DataContext dbContext, IConfiguration config)
        {
            _context = dbContext;
            _config = config;
        }
        public Task<UserEntity> CreateUser(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DoesEmailExist(string email) => await _context.Users.SingleOrDefaultAsync(user => user.Email == email) != null;

        public async Task<bool> DoesUsernameExist(string username) => await _context.Users.SingleOrDefaultAsync(user => user.Username == username) != null;
        
    }
}