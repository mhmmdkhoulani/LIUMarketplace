using LIUMarketplace.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.UI.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> RegisterUserAsync(RegisterDto model);

    }
}
