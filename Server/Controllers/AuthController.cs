using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.DTOModels.AuthDTO;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignUpService signUpService;
        private readonly SignInService signInService;

        public AuthController(SignUpService signUpService, SignInService signInService)
        {
            this.signUpService = signUpService;
            this.signInService = signInService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDTO request)
        {
            if (request == null)
            {
                return BadRequest("Пользовательские данные не переданы.");
            }

            try
            {
                await signUpService.SignUp(request);
                return CreatedAtAction(nameof(SignUp), new { email = request.Email }, request);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка регистрации: {ex.Message}");
            }
        }

        [HttpGet("signin")]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            if (email.IsNullOrEmpty())
            {
                return BadRequest("Email не передан.");
            }
            if (password.IsNullOrEmpty())
            {
                return BadRequest("Password не передан.");
            }

            var request = new LoginDTO(email, password);
            try
            {
                var user = await signInService.SignIn(request);
                return Ok(new { message = "Успешный вход", user }); 
            }
            catch (Exception ex)
            {
                return Unauthorized($"Ошибка входа: {ex.Message}");
            }
        }
    }
}