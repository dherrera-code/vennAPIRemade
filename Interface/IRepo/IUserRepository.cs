using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Interface.IRepo
{
    public interface IUserRepository
    {
        Task<UserEntity> CreateUser(UserEntity user);
        Task<bool> DoesUsernameExist(string username);
        Task<bool> DoesEmailExist(string email);
        Task<UserEntity> GetUserByUsernameOrEmail(string username);
        Task<IEnumerable<UserEntity>> GetAllUsers();
        Task<UserEntity> GetUserById(int userId);
        Task<UserEntity> UpdateUserInfo(UserEntity user);
        Task<bool> DeleteUser(int id);
    }
}