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
        Task<PagedList<ProductDetailsDto>> GetAllProductAsync(string query = "", int page = 1, int pageSize = 12);
        Task<IEnumerable<ProductDetailsDto>> GetAllProductByCategoryIdAsync(int id);
        Task<ProductDetailsDto> GetProductByIdAsync(string id);
        Task<bool> IsValidCategoryId(int id);
        Task DeleteProductAsync(string id);
        Task<ProductDetailsDto> UpdateMovieAsync(ProductDto product);
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

        public async Task<PagedList<ProductDetailsDto>> GetAllProductAsync(string query = "", int page = 1, int pageSize = 12)
        {
            if (string.IsNullOrWhiteSpace(query))
                query = "";
            if (page < 1)
                page = 1;
            if (pageSize < 5)
                pageSize = 5;
            if (pageSize > 50)
                pageSize = 50;

            var products = await (from p in _db.Products
                                  where p.CreatedByUserId == _identityOption.UserId 
                               && (p.Name.Contains(query)
                                  || p.Description.Contains(query))
                               orderby p.CreatedTime descending
                               select p).ToArrayAsync();
           
            var pagedList = new PagedList<ProductDetailsDto>(products.Select(p => p.ToProductDetailsDto()),page,pageSize);
            return pagedList;
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetAllProductByCategoryIdAsync(int id)
        {
            var products = _db.Products
               .OrderByDescending(p => p.CreatedTime)
               .Include(p => p.Category)
               .Where(p => p.CategoryId == id);

            return await products.Select(p => p.ToProductDetailsDto()).ToListAsync();
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

        public async Task<ProductDetailsDto> UpdateMovieAsync(ProductDto dto)
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
