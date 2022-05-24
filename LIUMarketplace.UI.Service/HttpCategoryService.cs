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
    public class HttpCategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public HttpCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("/api/categories");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
                return result;
            }
            else
            {
                throw new Exception("Error");
            }
        }
    }
}
