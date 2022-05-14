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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        #region GetReviewsByProduct
        [HttpGet("productid/{id}")]

        public async Task<IActionResult> GetAllAsync(string id)
        {
            var isValid = await _reviewService.IsValidProductId(id); 
            if (!isValid)
            {
                return BadRequest("There is no product found");
            }

            var reviews = await _reviewService.GetReviewsByProductIdAsync(id);

            if(reviews.Count() == 0)
            {
                return NotFound("No reviews");
            }
          
            return Ok(reviews);

        }

        #endregion

        #region GetReviewsByUserId 
        [HttpGet]
        public async Task<IActionResult> GetAllByUserIdAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviews = await _reviewService.GetAllReviewsByUserAsync(userId);

            return Ok(reviews);
        }
        #endregion

        #region AddReview 
        [HttpPost]
        public async Task<IActionResult> AddReviewAsync([FromBody] ReviewDto dto)
        {
            
            var result = await _reviewService.AddReviewAsync(dto);

            if(result != null)
                return Ok(result);
            return BadRequest("Review not added");
        }
        #endregion

        #region DeleteReview 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviewAsync(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var review = await _reviewService.GetReviewAsync(id);
            if (review.UserId != userId)
            {
                return BadRequest("You do not have permission");
            }

            if (review == null)
                return BadRequest($"No review with this id {id}");

            await _reviewService.DeleteReviewAsync(id);
            return Ok("Review Deleted");
        }
        #endregion

        #region UpdateReview
        [HttpPut]
        public async Task<IActionResult> UpdateReviewAsync([FromForm] ReviewDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var review = await _reviewService.GetReviewAsync(model.Id);

            if (review == null)
                return BadRequest($"Review with id {model.Id} not found");

            if(review.UserId != userId)
            {
                return BadRequest("You do not have permission");
            }

            var isValid = await _reviewService.IsValidProductId(model.ProductId);

            if (!isValid)
                return BadRequest("Invalid Product");

            var result = await _reviewService.UpdateReviewAsync(model);
            return Ok(result);
        }
        #endregion
    }
}
