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

        public async Task<ProductDetailsDto> AddProductAsynv(ProductDto dto, FormFile coverFile)
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

        public async Task<ProductDetailsDto> EditProductAsynv(ProductDto dto, FormFile coverFile)
        {
            var form = PrepareProductForm(dto, coverFile, false);
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

        private HttpContent PrepareProductForm(ProductDto dto, FormFile file, bool isUpdate)
        {
            var form = new MultipartFormDataContent();


            form.Add(new StringContent(dto.Name), nameof(ProductDto.Name));
            form.Add(new StringContent(dto.Description), nameof(ProductDto.Description));
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
