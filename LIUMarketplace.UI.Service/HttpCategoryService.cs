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

        public async Task<CategoryDto> AddCategoryAsync(CategoryDto dto)
        {
            
              var response = await _httpClient.PostAsJsonAsync("/api/categories/add", dto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CategoryDto>();
                return result;

            }
            else
            {
                throw new Exception("Error while adding new category");
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
           
            var result = await _httpClient.DeleteAsync($"/api/categories/{id}");
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error while deleting the Category");
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

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CategoryDto>();
                return result;
            }
            else
            {
                throw new Exception("Error");
            }
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/categories/edit", dto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CategoryDto>();
                return result;

            }
            else
            {
                throw new Exception("Error while editing category");
            }
        }

       
    }
}
