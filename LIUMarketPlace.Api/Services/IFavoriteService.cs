using LIUMarketplace.Models;
using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;
using LIUMarketPlace.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LIUMarketPlace.Api.Services
{
    public interface IFavoriteService
    {
        Task<FavoriteDto> CreateFavoriteAsync();
        Task<FavoriteDto> GetFavoriteAsync(string userId);
        Task<FavoriteItemDto> GetFavoriteItemAsync(string productId);
        Task<IEnumerable<FavoriteItemDto>> GetFavoriteItemsAsync(string userId);
        Task<FavoriteItemDto> AddItemToFavoriteAsync(FavoriteItemDto Item);
        Task RemoveItemFromFavoriteAsync(string produtId);
        Task DeleteFavortieAsync(string id);
    }

    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _db;
        private readonly IdentityOption _identityOption;

        public FavoriteService(ApplicationDbContext db, IdentityOption identityOption)
        {
            _db = db;
            _identityOption = identityOption;
        }

        public async Task<FavoriteItemDto> AddItemToFavoriteAsync(FavoriteItemDto item)
        {
            var favoriteItem = new FavoriteItem
            {
                FavoriteId = item.FavoriteId,
                ProductId = item.ProductId,
            };

            await _db.FavoriteItems.AddAsync(favoriteItem);
            await _db.SaveChangesAsync();

            return new FavoriteItemDto
            {
                FavoriteId = favoriteItem.FavoriteId,
                ProductId = item.ProductId,
            };
        }

        public async Task<FavoriteDto> CreateFavoriteAsync()
        {
            var favorite = new Favorite
            {
                Id = Guid.NewGuid().ToString(),
                UserId = _identityOption.UserId,
            };
            await _db.Favorites.AddAsync(favorite);
            await _db.SaveChangesAsync();

            var favoriteDto = new FavoriteDto
            {
                FavoriteId = favorite.Id,
                UserId = favorite.UserId,
            };

            return favoriteDto;
        }

        public async Task DeleteFavortieAsync(string id)
        {
            var res = await _db.Favorites.FindAsync(id);
            if (res != null)
            {
                _db.Favorites.Remove(res);
                _db.SaveChanges();
            }
        }

        public async Task<FavoriteDto> GetFavoriteAsync(string userId)
        {
            var favorite = await _db.Favorites.Where(c => c.UserId == userId).FirstOrDefaultAsync();

            if (favorite != null)
            {
                var favoriteDto = new FavoriteDto
                {
                    UserId = favorite.UserId,
                    FavoriteId = favorite.Id
                };
                return favoriteDto;
            }
            return null;
        }

        public async Task<FavoriteItemDto> GetFavoriteItemAsync(string productId)
        {
            var item = await _db.FavoriteItems.Where(c => c.ProductId == productId).FirstOrDefaultAsync();
            if (item != null)
            {
                return item.ToFavoriteItmeDto();
            }
            return null;
            
        }

        public async Task<IEnumerable<FavoriteItemDto>> GetFavoriteItemsAsync(string userId)
        {
            var favorite = await _db.Favorites.Where(c => c.UserId == userId).FirstOrDefaultAsync();
            if (favorite != null)
            {
                var favoriteItems = _db.FavoriteItems.Where(c => c.FavoriteId == favorite.Id);
                var items = await favoriteItems.Select(i => i.ToFavoriteItmeDto()).ToListAsync();
                return items;
            }
            return null;
        }

        public async Task RemoveItemFromFavoriteAsync(string productId)
        {
            var favoriteItem = await _db.FavoriteItems.Where(c => c.ProductId == productId).FirstOrDefaultAsync();
            if (favoriteItem != null)
            {
                _db.FavoriteItems.Remove(favoriteItem);
                _db.SaveChanges();
            }
            
        }
    }
}
