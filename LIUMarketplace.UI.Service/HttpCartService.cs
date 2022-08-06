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
    public class HttpCartService : ICartService
    {
        private readonly HttpClient _httpClient;

        public HttpCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartItemDto> AddItemToCartAsync(CartItemDto cartItem)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/carts/additem", cartItem);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CartItemDto>();
                return result;

            }
            else
            {
                throw new Exception("Error while adding item to cart");
            }
        }

        public async Task ContactOwnerAsync(ConnectionRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/email", request);
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error while sending the message");

            
        }

        public async Task<CartDto> GetCartAsync()
        {
            var response = await _httpClient.GetAsync($"/api/carts");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CartDto>();
                return result;
            }
            else
            {
                throw new Exception("Error While getting the cart");
            }
        }

        public async Task<CartItemDto> GetCartItemAsync(string productId)
        {
            var response = await _httpClient.GetAsync($"/api/carts/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CartItemDto>();
                return result;
            }
            else
            {
                throw new Exception("Error While getting the item");
            }
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync()
        {
            var response = await _httpClient.GetAsync("/api/carts/items");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>();
                return result;
            }
            else
            {
                throw new Exception("Error While getting the items");
            }
        }

        public async Task RemoveItemToCartAsync(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/carts/{id}");
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error while deleting the item");
        }
    }
}
