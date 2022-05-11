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

        #region AddProduct
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.AddProductAsync(model);

            if (result == null)
            {
                return BadRequest("Product not Added ");
            }

            return Ok(result);
        }
        #endregion
    }
}
