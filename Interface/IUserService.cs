using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Interface
{
    public interface IUserService
    {
        public Task<UserDTO> CreateUser(NewUserDTO Dto);

    }
}