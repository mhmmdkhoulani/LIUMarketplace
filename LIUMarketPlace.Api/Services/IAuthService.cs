
using LIUMarketplace.Shared.DTOs;

namespace LIUMarketPlace.Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> CreateUserAsync(RegisterDto user, string role);
        Task<AuthResponse> LoginUserAsync(LoginDto model);
    }
}
