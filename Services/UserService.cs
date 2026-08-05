using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vennAPIRemade.Interface;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO> CreateUser(NewUserDTO Dto)
        {
            // Validate whether username is currently in use.
            bool result = await _userRepository.DoesUsernameExist(Dto.Username);
            if(result || await _userRepository.DoesEmailExist(Dto.Email) )
            {
                throw new Exception("Username or Email is in current use. Unable to Create Account");
            }

            // map out new entity
            UserEntity newUser = new();
            newUser.Username = Dto.Username;
            newUser.Email = Dto.Email;
            // Add functions to generate Salt and Hash functions
            
            UserEntity user = await _userRepository.CreateUser(newUser);
            if(user is null)
                throw new Exception("Unable to create new user. Try again later.");
            throw new NotImplementedException();
        }

        // Helper Functions
    }
}