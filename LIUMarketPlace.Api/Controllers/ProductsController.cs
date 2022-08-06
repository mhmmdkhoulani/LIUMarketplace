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
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IFavoriteService _favoriteService;

        public ProductsController(IProductService productService, ICartService cartService, IFavoriteService favoriteService)
        {
            _productService = productService;
            _cartService = cartService;
            _favoriteService = favoriteService;
        }

        #region GetAllProducts
        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllProductAsync();
            return Ok(products);

        }

        #endregion

        #region GetAllProductsByCategoryId
        [HttpGet("GetByCategoryId/{id}")]

        public async Task<IActionResult> GetAllByCategoryIdAsync(int id)
        {
            var isValid = await _productService.IsValidCategoryId(id);
            if (!isValid)
                return BadRequest("Invalid Category Id");

            var products = await _productService.GetAllProductByCategoryIdAsync(id);
            return Ok(products);
        }

        #endregion

        #region GetProductById
        [HttpGet("{id}")]

        public async Task<IActionResult> GetAsync( string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if(product == null)
            {
                return BadRequest($"Product with id {id} not found");
            }
            return Ok(product);
        }

        #endregion
        #region GetProductByCampus
        [HttpGet("campus/{campus}")]

        public async Task<IActionResult> GetByCampusAsync(string campus)
        {
            var product = await _productService.GetAllProductByCampusAsync(campus);
            if (product == null)
            {
                return BadRequest($"Products in {campus} not found");
            }
            return Ok(product);
        }

        #endregion

        #region GetProductByCart
        [HttpGet("cart")]

        public async Task<IActionResult> GetByCartAsync()
        {
            
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cartId = string.Empty;
            var result = await _cartService.GetCartAsync(userId);


            if (result != null)
            {
                cartId = result.CartId;
            }
            else
            {
                var cart = await _cartService.CreateCartAsync();
                cartId = cart.CartId;
            }


            var product = await _productService.GetAllProductByCartAsync(cartId);

            if (product == null)
            {
                return BadRequest($"Products in {cartId} not found");
            }
            return Ok(product);
        }

        #endregion

        #region GetProductByFavorite
        [HttpGet("favorite")]

        public async Task<IActionResult> GetByFavoriteAsync()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var favoriteId = string.Empty;
            var result = await _favoriteService.GetFavoriteAsync(userId);


            if (result != null)
            {
                favoriteId = result.FavoriteId;
            }
            else
            {
                var favorite = await _favoriteService.CreateFavoriteAsync();
                favoriteId = favorite.FavoriteId;
            }


            var product = await _productService.GetAllProductByFavoriteAsync(favoriteId);

            if (product == null)
            {
                return BadRequest($"Products in {favoriteId} not found");
            }
            return Ok(product);
        }

        #endregion

        #region AddProduct
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductDto model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isValid = await _productService.IsValidCategoryId(model.CategoryId);
            if (!isValid)
                return BadRequest("Invalid Category Id");

            var result = await _productService.AddProductAsync(model);
            return Ok(result);
        }
        #endregion

        #region UpdateProduct
        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromForm]ProductDto model)
        {
            var product = await _productService.GetProductByIdAsync(model.Id);

            if (product == null)
                return BadRequest($"Product with id {model.Id} not found");

            var isValid = await _productService.IsValidCategoryId(model.CategoryId);

            if (!isValid)
                return BadRequest("Invalid category ");

            var result = await _productService.UpdateProductAsync(model);
            return Ok(result);
        }
        #endregion

        #region DeleteProduct 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return BadRequest($"No product with this id {id}");

            await _productService.DeleteProductAsync(id);
            return Ok("Product Deleted");

        }
        #endregion
    }
}
