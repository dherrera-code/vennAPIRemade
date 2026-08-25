using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Interface.IService
{
    public interface IAuthService
    {
        Task<UserDTO> CreateUser(NewUserDTO Dto);
        Task<string> Login(LoginDTO userLogin);
    }
}