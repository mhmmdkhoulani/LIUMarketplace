using LIUMarketplace.Models;
using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;
using LIUMarketPlace.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LIUMarketPlace.Api.Services
{
    public interface ICartService
    {
        Task<CartDto> CreateCartAsync();
        Task<CartDto> GetCartAsync(string userId);
        Task<CartItemDto> GetCartItemAsync(string prodcutId);
        Task<IEnumerable<CartItemDto>> GetCartItemsAsync(string userId);
        Task<CartItemDto> AddItemToCartAsync(CartItemDto cartItem);
        Task RemoveItemToCartAsync(string produtId);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(string id);

    }

    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _db;
        private readonly IdentityOption _identityOption;

        public CartService(IdentityOption identityOption, ApplicationDbContext db)
        {
            _identityOption = identityOption;
            _db = db;
        }

        public async Task<CartItemDto> AddItemToCartAsync(CartItemDto item)
        {
            var cartItem = new CartItem
            {
                CartId = item.CartId,
                ProductId = item.ProductId,
            };

            await _db.CartItems.AddAsync(cartItem);
            await _db.SaveChangesAsync();

            return new CartItemDto
            {
                CartId = cartItem.CartId,
                ProductId = item.ProductId,
            };
        }

        public async Task<CartDto> CreateCartAsync()
        {
            var cart = new Cart
            {
                Id = Guid.NewGuid().ToString(),
                UserId = _identityOption.UserId,
            };
            await _db.Carts.AddAsync(cart);
            await _db.SaveChangesAsync();

            var cartDto = new CartDto
            {
                CartId = cart.Id,
                UserId = cart.UserId,
            };

            return cartDto;
        }
        public async Task DeleteCartAsync(string id)
        {
            var res = await _db.Carts.FindAsync(id);
            if(res != null)
            {
                _db.Carts.Remove(res);
                _db.SaveChanges();
            }
        }

        public async Task<CartDto> GetCartAsync(string userId)
        {
            var cart = await _db.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();
            
            if (cart != null)
            {
                var cartDto = new CartDto
                {
                    UserId = cart.UserId,
                    CartId = cart.Id
                };
                return cartDto;
            }
            return null;
        }

        public async Task<CartItemDto> GetCartItemAsync(string productId)
        {
            var item = await _db.CartItems.Where(c => c.ProductId == productId).FirstOrDefaultAsync();
            if (item != null)
            {
                return item.ToCartItmeDto();
            }
            return null;
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync(string userId)
        {
            var cart = await _db.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();
            if(cart != null)
            {
                var cartItems =  _db.CartItems.Where(c => c.CartId == cart.Id);
                var items = await cartItems.Select(i => i.ToCartItmeDto()).ToListAsync();
                return items;
            }
            return null;
            
        }

        public async Task RemoveItemToCartAsync(string productId)
        {
            var cartItem =  await _db.CartItems.Where(c => c.ProductId == productId).FirstOrDefaultAsync();
            if (cartItem != null)
            {
                _db.CartItems.Remove(cartItem);
                _db.SaveChanges();
            }
        }

        public Task<Cart> UpdateCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        
    }
}
