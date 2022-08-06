using LIUMarketplace.Shared.DTOs;
using LIUMarketPlace.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIUMarketPlace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMailService _mailService;

        public AuthController(IAuthService authService, IMailService mailService)
        {
            _authService = authService;
            _mailService = mailService;
        }

        // /api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.CreateUserAsync(model, "User");

            if (!result.IsAuthenticated)
            {
                return BadRequest(result);
            }

            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\EmailTemplate.html";
            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText.Replace("[email]", model.Email);

            await _mailService.SendEmailAsync(model.Email, "Welcome To LIU Marketplace", mailText);
            return Ok(result);
        }

        // /api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var result = await _authService.LoginUserAsync(model);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
