using LIUMarketplace.Models;
using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;

namespace LIUMarketPlace.Api.Services
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(AddProductDto dto);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        private readonly IdentityOption _identityOption;
        public ProductService(ApplicationDbContext db, IdentityOption identityOption)
        {
            _db = db;
            _identityOption = identityOption;
        }


        public async Task<Product> AddProductAsync(AddProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Status = dto.Status,
                CreatedByUserId = _identityOption.UserId,
            };

            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();

            return product;
        }
    }
}
