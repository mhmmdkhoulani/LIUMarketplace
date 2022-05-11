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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
                return BadRequest(result.Messages);
            }

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
                return BadRequest(result.Messages);
            }

            return Ok(result);
        }
    }
}
