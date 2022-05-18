using LIUMarketplace.Shared.DTOs;
using LIUMarketplace.UI.Service.Exceptions;
using LIUMarketplace.UI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.UI.Service
{
    public class HttpAuthenticaionService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public HttpAuthenticaionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponse> RegisterUserAsync(RegisterDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                return result;

            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
    }
}
