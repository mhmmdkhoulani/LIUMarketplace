using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;

namespace LIUMarketPlace.Api.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDto ToReviewDto(this Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                Content = review.Content,
                ProductId = review.ProductId,
            };
        }

        public static Review ToReview(this ReviewDto dto)
        {
            return new Review
            {
                Content = dto.Content,
                ProductId = dto.ProductId,
            };
        }

        public static ReviewDetailsDto ToReviewDetailsDto(this Review review)
        {
            return new ReviewDetailsDto
            {
                Id = review.Id,
                Content = review.Content,
                UserId = review.CreatedByUserId,
                CreatedTime = review.CreatedTime,
                ProductId = review.ProductId
            };
        }
    }
}
