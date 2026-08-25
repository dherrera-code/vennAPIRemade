using Microsoft.EntityFrameworkCore;
using vennAPIRemade.Models.Entity;
using vennAPIRemade.Context;
using vennAPIRemade.Interface.IRepo;

namespace vennAPIRemade.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;
        private readonly IConfiguration _config;
        public UserRepository(DataContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }
        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            await _dbContext.Users.AddAsync(user);
            var result = await _dbContext.SaveChangesAsync();
            if (result != 0)
                return user;
            else
                throw new Exception("Unable to Create New User.");
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userToDelete = await GetUserById(id);
            userToDelete.IsDeleted = true;
            _dbContext.Users.Update(userToDelete);
            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> DoesEmailExist(string email) => await _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email) != null;

        public async Task<bool> DoesUsernameExist(string username) => await _dbContext.Users.SingleOrDefaultAsync(user => user.Username == username) != null;

        public async Task<IEnumerable<UserEntity>> GetAllUsers()
        {
            return await _dbContext.Users.AsNoTracking()
            .Where(u => u.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<UserEntity> GetUserById(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<UserEntity> GetUserByUsernameOrEmail(string username)
        {
            return await _dbContext.Users.AsNoTracking().Where(u => u.IsDeleted == false).FirstOrDefaultAsync(user => user.Username == username || user.Email == username);
        }

        public async Task<UserEntity> UpdateUserInfo(UserEntity user)
        {
            _dbContext.Users.Update(user);
            bool success = await _dbContext.SaveChangesAsync() != 0;
            if (!success) throw new Exception("Unable to update user!");

            return user;
        }
    }
}