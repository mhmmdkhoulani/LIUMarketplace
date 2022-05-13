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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region GetAllCategories
        [HttpGet]
        public async Task<IActionResult> GetAllCategoies()
        {

            var categories = await _categoryService.GetAllCategoriesAsync();

            return Ok(categories);

        }
        #endregion

        #region AddCategory
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.AddCategoryAsync(model);

            if (result == null)
            {
                return BadRequest("Category not Added ");
            }

            return Ok(result);
        }
        #endregion


        #region EditCategory
        [Authorize(Roles = "Admin")]
        [HttpPut("edit")]
        public async Task<IActionResult> EditCategory([FromBody] Category model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.UpdateCategoryAsync(model);

            if (result == null)
            {
                return BadRequest("Category is not found");
            }

            return Ok(result);
        }
        #endregion

        #region DeleteCategory
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromBody] int id)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.GetCategoryByIdAsync(id);

            if (result == null)
            {
                return BadRequest($"Category with id {id} is not found")    ;
            }
            else
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok($"{result.Name} deleted");
            }

            
        }
        #endregion


    }
}
