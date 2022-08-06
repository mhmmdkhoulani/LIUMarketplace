using LIUMarketplace.Models;
using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;
using LIUMarketPlace.Api.Helpers;
using LIUMarketPlace.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LIUMarketPlace.Api.Services
{
    public interface IProductService
    {
        Task<ProductDetailsDto> AddProductAsync(ProductDto dto);
        Task<IEnumerable<ProductDetailsDto>> GetAllProductAsync();
        Task<IEnumerable<ProductDetailsDto>> GetAllProductByCampusAsync(string campus);
        Task<IEnumerable<ProductDetailsDto>> GetAllProductByCartAsync(string cart);
        Task<IEnumerable<ProductDetailsDto>> GetAllProductByFavoriteAsync(string favorite);
        Task<IEnumerable<ProductDetailsDto>> GetAllProductByCategoryIdAsync(int id);
        Task<ProductDetailsDto> GetProductByIdAsync(string id);
        Task<bool> IsValidCategoryId(int id);
        Task DeleteProductAsync(string id);
        Task<ProductDetailsDto> UpdateProductAsync(ProductDto product);
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


        public async Task<ProductDetailsDto> AddProductAsync(ProductDto dto)
        {

            var productImage = await FileHelper.ImageUpload(dto.ImageCover);

            var product = dto.ToProduct();
            product.ImageCoverUrl = productImage;
            product.CreatedByUserId = _identityOption.UserId;
            product.Category = await _db.Categories.FindAsync(dto.CategoryId);

            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();

            return product.ToProductDetailsDto();
        }

        public async Task DeleteProductAsync(string id)
        {
            var product = await _db.Products.FindAsync(id);
            _db.Products.Remove(product);
            _db.SaveChanges();
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetAllProductAsync()
        {
            var products = _db.Products
               .OrderByDescending(p => p.CreatedTime)
               .Include(p => p.Category);

            return await products.Select(p => p.ToProductDetailsDto()).ToListAsync();
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetAllProductByCampusAsync(string campus)
        {
            var products = await (from p in _db.Products
                                  .Include(p => p.Category)
                                  join u in _db.Users
                                  on p.CreatedByUserId equals u.Id
                                  where u.Campus == campus

                                  select p).ToArrayAsync();
                                  

            return  products.Select(p => p.ToProductDetailsDto()).ToList();
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetAllProductByCartAsync(string cart)
        {
            var products = await(from p in _db.Products
                                 .Include(p => p.Category)
                                 join ci in _db.CartItems
                                 on p.Id equals ci.ProductId
                                 where ci.CartId == cart

                                 select p).ToArrayAsync();


            return products.Select(p => p.ToProductDetailsDto()).ToList();
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetAllProductByCategoryIdAsync(int id)
        {
            var products = _db.Products
               .OrderByDescending(p => p.CreatedTime)
               .Include(p => p.Category)
               .Where(p => p.CategoryId == id);

            return await products.Select(p => p.ToProductDetailsDto()).ToListAsync();
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetAllProductByFavoriteAsync(string favorite)
        {
            var products = await(from p in _db.Products
                                 .Include(p => p.Category)
                                 join f in _db.FavoriteItems
                                 on p.Id equals f.ProductId
                                 where f.FavoriteId == favorite

                                 select p).ToArrayAsync();


            return products.Select(p => p.ToProductDetailsDto()).ToList();
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(string id)
        {
            var product = await _db.Products.FindAsync(id);

            if(product != null)
            {
                var cateoryId = product.CategoryId;
                product.Category = await _db.Categories.FindAsync(cateoryId);
                return product.ToProductDetailsDto();
            }
            return null;
        }

        public async Task<bool> IsValidCategoryId(int id)
        {
            var result = await _db.Categories.AnyAsync(c => c.Id == id);
            return result;
        }

        public async Task<ProductDetailsDto> UpdateProductAsync(ProductDto dto)
        {
            var product = await _db.Products.FindAsync(dto.Id);

            if(product == null)
            {
                return null;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Status = dto.Status;
            product.CategoryId = dto.CategoryId;

            if (product.ImageCover != null)
            {
                var productImage = await FileHelper.ImageUpload(dto.ImageCover);
                product.ImageCoverUrl = productImage;
            }

            await _db.SaveChangesAsync();
            return product.ToProductDetailsDto();

        }
    }
}
