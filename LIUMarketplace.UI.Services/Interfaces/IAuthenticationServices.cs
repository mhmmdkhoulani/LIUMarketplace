using LIUMarketplace.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.UI.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<AuthResponse> RegisterUserAsync(RegisterDto model);
    }
}
