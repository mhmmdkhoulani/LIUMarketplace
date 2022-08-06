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
    public class HttpProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public HttpProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDetailsDto> AddProductAsyn(ProductDto dto, FormFile coverFile)
        {
            var form = PrepareProductForm(dto, coverFile, false);
            var response = await _httpClient.PostAsync("/api/products", form);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ProductDetailsDto>();
                return result;

            }
            else
            {
                throw new Exception("Error");
            }
        }

        public async Task DeleteProductAsync(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/products/{id}");
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error while deleting the product");
        }

        public async Task<ProductDetailsDto> EditProductAsyn(ProductDto dto, FormFile coverFile)
        {
            var form = PrepareProductForm(dto, coverFile, true);
            var response = await _httpClient.PutAsync("/api/products", form);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ProductDetailsDto>();
                return result;

            }
            else
            {
                throw new Exception("Error");
            }
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ProductDetailsDto>();
                return result;
            }
            else
            {
                throw new Exception("Error");
            }
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("/api/products");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDetailsDto>>();
                return result;
            }
            else
            {
                throw new Exception("Error");
            }
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetProductsByCampusAsync(string campus)
        {
            var response = await _httpClient.GetAsync($"/api/products/campus/{campus}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDetailsDto>>();
                return result;
            }
            else
            {
                throw new Exception("Error");
            }
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetProductsByCartAsync()
        {
            var response = await _httpClient.GetAsync($"/api/products/cart");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDetailsDto>>();
                return result;
            }
            else
            {
                throw new Exception("Error getting cart list");
            }
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var response = await _httpClient.GetAsync($"/api/products/getbycategoryid/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDetailsDto>>();
                return result;
            }
            else
            {
                throw new Exception("Error");
            }
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetProductsByFavoriteAsync()
        {
            var response = await _httpClient.GetAsync($"/api/products/favorite");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDetailsDto>>();
                return result;
            }
            else
            {
                throw new Exception("Error getting favorite list");
            }
        }

        private HttpContent PrepareProductForm(ProductDto dto, FormFile file, bool isUpdate)
        {
            var form = new MultipartFormDataContent();


            form.Add(new StringContent(dto.Name), nameof(ProductDto.Name));
            form.Add(new StringContent(dto.Description), nameof(ProductDto.Description));
            form.Add(new StringContent(dto.CategoryId.ToString()), nameof(ProductDto.CategoryId));
            form.Add(new  StringContent(dto.Price.ToString()), nameof(ProductDto.Price));
            form.Add(new  StringContent("Availabe"), nameof(ProductDto.Status));
            if (isUpdate)
                form.Add(new StringContent(dto.Id), nameof(ProductDto.Id));
            if(file != null)
                form.Add(new StreamContent(file.FileStream), nameof(ProductDto.ImageCover), file.FileName);
           
            return form;

        }
    }
}
