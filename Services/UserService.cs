using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            if (result || await _userRepository.DoesEmailExist(Dto.Email))
            {
                throw new Exception("Username or Email is in current use. Unable to Create Account");
            }


            // map out new entity
            UserEntity newUser = new();
            PasswordDTO EncryptedPassword = HashPassword(Dto.Password);
            newUser.Username = Dto.Username;
            newUser.Email = Dto.Email;
            newUser.Salt = EncryptedPassword.Salt;
            newUser.Hash = EncryptedPassword.Hash;

            UserEntity user = await _userRepository.CreateUser(newUser);
            if (user is null)
                throw new Exception("Unable to create new user. Try again later.");
            else
            {
                return new UserDTO
                {
                    Username = user.Username,
                    Email = user.Email,
                    AccountCreated = user.AccountCreated
                };
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var userList = await _userRepository.GetAllUsers();
            //install automapper

            throw new NotImplementedException();
        }

        // Helper Functions
        private static PasswordDTO HashPassword(string password)
        {
            byte[] SaltBytes = RandomNumberGenerator.GetBytes(64);

            string salt = Convert.ToBase64String(SaltBytes);
            string hash;

            using (var derivedBytes = new Rfc2898DeriveBytes(password, SaltBytes, 310000, HashAlgorithmName.SHA256))
            {
                hash = Convert.ToBase64String(derivedBytes.GetBytes(32));
            }
            return new PasswordDTO
            {
                Salt = salt,
                Hash = hash
            };
        }


    }
}