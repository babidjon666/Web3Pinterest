using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOModels.AuthDTO
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;

        public LoginDTO(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }
    }
}