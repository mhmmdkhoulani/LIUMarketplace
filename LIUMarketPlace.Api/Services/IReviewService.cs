using LIUMarketplace.Models;
using LIUMarketplace.Shared.DTOs;
using LIUMarketPlace.Api.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LIUMarketPlace.Api.Services
{
    public interface IReviewService
    {
        Task<ReviewDetailsDto> AddReviewAsync(ReviewDto dto);
        Task<IEnumerable<ReviewDetailsDto>> GetAllReviewsByUserAsync(string id);
        Task<IEnumerable<ReviewDetailsDto>> GetReviewsByProductIdAsync(string id);
        Task<ReviewDetailsDto> GetReviewAsync(string id);
        Task<bool> IsValidProductId(string id);
        Task DeleteReviewAsync(string id);
        Task<ReviewDetailsDto> UpdateReviewAsync(ReviewDto dto);
    }

    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _db;
        private readonly IdentityOption _identityOption;

        public ReviewService(ApplicationDbContext db, IdentityOption identityOption)
        {
            _db = db;
            _identityOption = identityOption;
        }

        public async Task<ReviewDetailsDto> AddReviewAsync(ReviewDto dto)
        {
            var review = dto.ToReview();
            review.CreatedByUserId = _identityOption.UserId;

            await _db.Reviews.AddAsync(review);
            await _db.SaveChangesAsync();

            return review.ToReviewDetailsDto();
        }

        public async Task DeleteReviewAsync(string id)
        {
            var review = await _db.Reviews.FindAsync(id);
            _db.Reviews.Remove(review);
            _db.SaveChanges();  
        }

        public async Task<IEnumerable<ReviewDetailsDto>> GetAllReviewsByUserAsync(string id)
        {
            var reviews = _db.Reviews
                .OrderByDescending(r => r.CreatedTime)
                .Include(r => r.Product)
                .Where(r => r.CreatedByUserId == id);

            return await reviews.Select(r => r.ToReviewDetailsDto()).ToListAsync();

        }

        public async Task<ReviewDetailsDto> GetReviewAsync(string id)
        {
            var review = await _db.Reviews.FindAsync(id);

            if (review != null)
            {
                
                return review.ToReviewDetailsDto();
            }
            return null;
        }

        public async Task<IEnumerable<ReviewDetailsDto>> GetReviewsByProductIdAsync(string id)
        {
            var reviews = _db.Reviews
                .OrderByDescending(r => r.CreatedTime)
                .Include(r => r.Product)
                .Where(r => r.ProductId == id);
            if (reviews == null)
                return null;

            return await reviews.Select(r => r.ToReviewDetailsDto()).ToListAsync();
        }

        public async Task<bool> IsValidProductId(string id)
        {
            var result = await _db.Products.AnyAsync(c => c.Id == id);
            return result;
        }

        public async Task<ReviewDetailsDto> UpdateReviewAsync(ReviewDto dto)
        {
            var review = await _db.Reviews.FindAsync(dto.Id);

            if (review == null)
            {
                return null;
            }
            review.Content = dto.Content;
       
            await _db.SaveChangesAsync();
            return review.ToReviewDetailsDto();
        }
    }
}
