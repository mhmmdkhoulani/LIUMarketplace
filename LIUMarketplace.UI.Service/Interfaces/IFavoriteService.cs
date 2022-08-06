using LIUMarketplace.Shared.DTOs;

namespace LIUMarketplace.UI.Service.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteItemDto>> GetFavoriteItemsAsync();
        Task<FavoriteItemDto> AddItemToFavoriteAsync(FavoriteItemDto FavoriteItem);
        Task RemoveItemFromFaavoriteAsync(string productId);
        Task<FavoriteItemDto> GetFavoritetemAsync(string productId);
        Task<FavoriteDto> GetFavoriteAsync();
    }
}
