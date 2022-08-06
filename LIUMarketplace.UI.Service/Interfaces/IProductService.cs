using LIUMarketplace.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.UI.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetailsDto>> GetProductsAsync();
        Task<IEnumerable<ProductDetailsDto>> GetProductsByCampusAsync(string campus);
        Task<IEnumerable<ProductDetailsDto>> GetProductsByCartAsync();
        Task<IEnumerable<ProductDetailsDto>> GetProductsByFavoriteAsync();
        Task<IEnumerable<ProductDetailsDto>> GetProductsByCategoryAsync(int id);
        Task<ProductDetailsDto> GetProductByIdAsync(string id);
        Task<ProductDetailsDto> AddProductAsyn(ProductDto dto, FormFile coverFile);
        Task<ProductDetailsDto> EditProductAsyn(ProductDto dto, FormFile coverFile);
        Task DeleteProductAsync(string id);
    }
}
