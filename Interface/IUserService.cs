using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Interface
{
    public interface IUserService
    {
        Task<bool> DeleteUser(int id);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int id);
        Task<UserDTO> UpdateUser(string userId, UserDTO updatedUser);
    }
}