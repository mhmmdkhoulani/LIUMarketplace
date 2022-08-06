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
    public class HttpFavoriteService : IFavoriteService
    {
        private readonly HttpClient _httpClient;

        public HttpFavoriteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FavoriteItemDto> AddItemToFavoriteAsync(FavoriteItemDto favoriteItem)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/favorites/additem", favoriteItem);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<FavoriteItemDto>();
                return result;

            }
            else
            {
                throw new Exception("Error while adding item to favorite");
            }
        }

        public async Task<FavoriteDto> GetFavoriteAsync()
        {
            var response = await _httpClient.GetAsync($"/api/favorites");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<FavoriteDto>();
                return result;
            }
            else
            {
                throw new Exception("Error While getting the favorite");
            }
        }

        public async Task<IEnumerable<FavoriteItemDto>> GetFavoriteItemsAsync()
        {
            var response = await _httpClient.GetAsync("/api/favorites/items");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<FavoriteItemDto>>();
                return result;
            }
            else
            {
                throw new Exception("Error While getting the items");
            }
        }

        public async Task<FavoriteItemDto> GetFavoritetemAsync(string productId)
        {
            var response = await _httpClient.GetAsync($"/api/favorites/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<FavoriteItemDto>();
                return result;
            }
            else
            {
                throw new Exception("Error While getting the item");
            }
        }

        public async Task RemoveItemFromFaavoriteAsync(string productId)
        {
            var result = await _httpClient.DeleteAsync($"/api/favorites/{productId}");
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error while deleting the item");
        }
    }
}
