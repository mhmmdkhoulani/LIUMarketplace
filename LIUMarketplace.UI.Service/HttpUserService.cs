using LIUMarketplace.Shared.DTOs;
using LIUMarketplace.UI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.UI.Service
{

    public class HttpUserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public HttpUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public  async Task DeleteUserAsync(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/users/{id}");
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error while deleting the user");
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("/api/users");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();
                return result;
            }
            else
            {
                throw new Exception("Error getting users");
            }
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserDto>();
                return result;
            }
            else
            {
                throw new Exception("Error");
            }
        }

        public async Task<UserDto> GetByProductIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/users/product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserDto>();
                return result;
            }
            else
            {
                throw new Exception("Error gettin user by product id ");
            }
        }
    }
}
