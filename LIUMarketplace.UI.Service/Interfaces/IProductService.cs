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
        Task<ProductDetailsDto> GetProductByIdAsync(string id);
        Task<ProductDetailsDto> AddProductAsynv(ProductDto dto, FormFile coverFile);
        Task<ProductDetailsDto> EditProductAsynv(ProductDto dto, FormFile coverFile);
    }
}
