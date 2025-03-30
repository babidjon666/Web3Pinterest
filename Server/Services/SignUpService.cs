using System;
using System.Threading.Tasks;
using Server.Models;
using Server.Repositories;
using System.Security.Cryptography;
using System.Text;
using Server.DTOModels.AuthDTO;

namespace Server.Services
{
    public class SignUpService
    {
        private readonly UserRepository userRepo;
        private readonly HashPasswordService passwordService;

        public SignUpService(UserRepository userRepo, HashPasswordService passwordService)
        {
            this.userRepo = userRepo;
            this.passwordService = passwordService;
        }

        public async Task SignUp(RegisterDTO registerDto)
        {
            bool isEmailTaken = await userRepo.CheckEmail(registerDto.Email);
            if (isEmailTaken)
            {
                throw new Exception("Email уже занят.");
            }

            bool isUserNameTaken = await userRepo.CheckUserName(registerDto.UserName);
            if (isUserNameTaken)
            {
                throw new Exception("Имя пользователя уже занято.");
            }

            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Password = passwordService.HashPassword(registerDto.Password)
            };

            await userRepo.AddUserToDataBase(user);
        }
    }
}