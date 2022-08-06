using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;
using LIUMarketPlace.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LIUMarketPlace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        #region GetCartItems
        [HttpGet("items")]
        public async Task<IActionResult> GetCartItems()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await _cartService.GetCartItemsAsync(userId);


            if (result != null)
            {
                return Ok(result);
            }
            

            return BadRequest("There is no items");
        }

        [HttpGet()]
        public async Task<IActionResult> GetCart()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await _cartService.GetCartAsync(userId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                var cart = await _cartService.CreateCartAsync();
                return Ok(cart);
            }

        }
        #endregion

        #region AddCartItem
        [HttpPost("additem")]
        public async Task<IActionResult> AddCartItem([FromBody] CartItemDto item)
        {
            string userId =  User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await _cartService.GetCartAsync(userId);
            
           
            if (result != null)
            {
                item.CartId = result.CartId;
            }
            else
            {
                var cart = await _cartService.CreateCartAsync();
                item.CartId = cart.CartId;
            }
            
            
            var cartitem = await _cartService.AddItemToCartAsync(item);

            if (cartitem == null)
            {
                return BadRequest("cartitem not Added ");
            }

            return Ok(cartitem);
        }
        #endregion

        #region DeleteCartItem
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var product = await _cartService.GetCartItemAsync(id);

            if (product == null)
                return BadRequest($"No item with this id {id}");

            await _cartService.RemoveItemToCartAsync(id);
            return Ok("Item Deleted");

        }
        #endregion

        #region GetCartItem
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemAsync(string id)
        {
            var item = await _cartService.GetCartItemAsync(id);

            if (item == null)
                return BadRequest($"No item with this id {id}");

            
            return Ok(item);

        }
        #endregion
    }
}
