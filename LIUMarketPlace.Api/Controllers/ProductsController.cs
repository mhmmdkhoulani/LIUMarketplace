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

        public ProductsController(IProductService productService)
        {
            _productService = productService;
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

            var result = await _productService.UpdateMovieAsync(model);
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
