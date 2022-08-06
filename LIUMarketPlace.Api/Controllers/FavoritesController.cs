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
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        #region GetFavoriteItems
        [HttpGet("items")]
        public async Task<IActionResult> GetFavoriteItems()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await _favoriteService.GetFavoriteItemsAsync(userId);


            if (result != null)
            {
                return Ok(result);
            }


            return BadRequest("There is no items");
        }

        [HttpGet()]
        public async Task<IActionResult> GetFavorite()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await _favoriteService.GetFavoriteAsync(userId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                var favorite = await _favoriteService.CreateFavoriteAsync();
                return Ok(favorite);
            }

        }
        #endregion

        #region AddFavoriteItem
        [HttpPost("additem")]
        public async Task<IActionResult> AddFavoriteItem([FromBody] FavoriteItemDto item)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await _favoriteService.GetFavoriteAsync(userId);


            if (result != null)
            {
                item.FavoriteId = result.FavoriteId;
            }
            else
            {
                var favorite = await _favoriteService.CreateFavoriteAsync();
                item.FavoriteId = favorite.FavoriteId;
            }


            var favoriteItem = await _favoriteService.AddItemToFavoriteAsync(item);

            if (favoriteItem == null)
            {
                return BadRequest("favorite item not Added ");
            }

            return Ok(favoriteItem);
        }
        #endregion

        #region DeleteFavoriteItem
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var product = await _favoriteService.GetFavoriteItemAsync(id);

            if (product == null)
                return BadRequest($"No item with this id {id}");

            await _favoriteService.RemoveItemFromFavoriteAsync(id);
            return Ok("Item Deleted");

        }
        #endregion

        #region GetFavoriteItem
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemAsync(string id)
        {
            var item = await _favoriteService.GetFavoriteItemAsync(id);

            if (item == null)
                return BadRequest($"No item with this id {id}");


            return Ok(item);

        }
        #endregion
    }
}
