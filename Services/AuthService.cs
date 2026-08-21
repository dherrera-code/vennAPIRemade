using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using vennAPIRemade.Interface;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }
        public async Task<UserDTO> CreateUser(NewUserDTO Dto)
        {
            // Validate whether username is currently in use.
            bool result = await _userRepository.DoesUsernameExist(Dto.Username);
            if (result || await _userRepository.DoesEmailExist(Dto.Email))
            {
                throw new Exception("Username or Email is in current use. Unable to Create Account.");
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

        public async Task<string> Login(LoginDTO userLogin)
        {
            var currentUser = await _userRepository.GetUserByUsernameOrEmail(userLogin.Username);

            if (currentUser is null) return null;

            if (!VerifyPassword(userLogin.Password, currentUser.Salt, currentUser.Hash))
                return null;

            var claims = new List<Claim>
            {
                new Claim("sub", currentUser.Id.ToString())
            };

            return GenerateJWT(claims);
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

        private static bool VerifyPassword(string password, string salt, string hash)
        {
            byte[] saltByte = Convert.FromBase64String(salt);
            string checkHash;
            using (var derivedBytes = new Rfc2898DeriveBytes(password, saltByte, 310000, HashAlgorithmName.SHA256))
            {
                checkHash = Convert.ToBase64String(derivedBytes.GetBytes(32));
                return hash == checkHash;
            }
        }

        private string GenerateJWT(List<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            var SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://vennbackendapi-akghachgbhgdccfe.westus3-01.azurewebsites.net",
                audience: "https://vennbackendapi-akghachgbhgdccfe.westus3-01.azurewebsites.net",
                claims: claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

    }
}