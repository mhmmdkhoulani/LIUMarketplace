using LIUMarketplace.Shared.DTOs;

namespace LIUMarketplace.UI.Service.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDto>> GetCartItemsAsync();
        Task<CartItemDto> AddItemToCartAsync(CartItemDto cartItem);
        Task RemoveItemToCartAsync(string productId);
        Task<CartItemDto> GetCartItemAsync(string productId);
        Task ContactOwnerAsync(ConnectionRequest request);
        Task<CartDto> GetCartAsync();
    }
}
