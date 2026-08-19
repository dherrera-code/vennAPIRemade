using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using vennAPIRemade.Interface;
using vennAPIRemade.Mapper;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteUser(int id)
        {
            bool result = await _userRepository.DeleteUser(id);
            return result;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var userList = await _userRepository.GetAllUsers();

            return _mapper.Map<IEnumerable<UserDTO>>(userList);
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            UserEntity user = await _userRepository.GetUserById(id);
            if(user is null) return null;

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByName(string username)
        {
            UserEntity user = await _userRepository.GetUserByUsernameOrEmail(username);
            return _mapper.Map<UserDTO>(user);

        }

        public async Task<UserDTO> UpdateUser(string userId, UserDTO updatedUser)
        {
            UserEntity user = await _userRepository.GetUserById(int.Parse(userId));

            // lets update user
            user.Username = updatedUser.Username;
            user.Description = updatedUser.Description;
            user.UserIcon = updatedUser.UserIcon;
            
            UserEntity updated = await _userRepository.UpdateUserInfo(user);

            if(updated is null) throw new Exception($"Unable to Update Profile. User Id is {userId}");

            return _mapper.Map<UserDTO>(updated);
        }
    }
}