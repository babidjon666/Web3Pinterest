using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.DTOModels.AuthDTO;
using Server.Models;
using Server.Repositories;

namespace Server.Services
{
    public class SignInService
    {
        private readonly UserRepository userRepository;
        private readonly HashPasswordService passwordService;

        public SignInService(UserRepository userRepository, HashPasswordService passwordService)
        {
            this.userRepository = userRepository;
            this.passwordService = passwordService;
        }

        public async Task<User> SignIn(LoginDTO request)
        {
            var hashedPassword = passwordService.HashPassword(request.Password);

            var user = await userRepository.GetUserFromDataBase(request.Email, hashedPassword);

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Не удалось выполнить вход.");
            }
        }
    }
}