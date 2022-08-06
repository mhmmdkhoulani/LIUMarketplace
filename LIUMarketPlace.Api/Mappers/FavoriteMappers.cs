using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;

namespace LIUMarketPlace.Api.Mappers
{
    public static class FavoriteMappers
    {
        public static FavoriteItemDto ToFavoriteItmeDto(this FavoriteItem item)
        {
            return new FavoriteItemDto
            {
                Id = item.Id,
                FavoriteId = item.FavoriteId,
                ProductId = item.ProductId,
            };
        }

        public static FavoriteItem ToFavoritItem(this FavoriteItemDto dto)
        {
            return new FavoriteItem
            {
                Id = dto.Id,
                FavoriteId = dto.FavoriteId,

            };
        }
    }
}
