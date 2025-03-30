using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOModels.AuthDTO
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = String.Empty;
        [Required]
        public string UserName { get; set; } = String.Empty;
    }
}