using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Interface
{
    public interface IUserRepository
    {
        Task<UserEntity> CreateUser(UserEntity user);
        Task<bool> DoesUsernameExist(string username);
        Task<bool> DoesEmailExist(string email);
    }
}