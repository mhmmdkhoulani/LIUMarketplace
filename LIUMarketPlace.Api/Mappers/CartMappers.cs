using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;

namespace LIUMarketPlace.Api.Mappers
{
    public static class CartMappers
    {
        public static CartItemDto ToCartItmeDto(this CartItem item)
        {
            return new CartItemDto
            {
                Id = item.Id,
                CartId = item.CartId,
                ProductId = item.ProductId, 
            };
        }

        public static CartItem ToCartItem(this CartItemDto dto)
        {
            return new CartItem
            {
                Id = dto.Id,
                CartId = dto.CartId,

            };
        }
    }
}
