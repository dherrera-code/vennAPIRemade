using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Interface
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(NewUserDTO Dto);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<string> Login(LoginDTO userLogin);
    }
}