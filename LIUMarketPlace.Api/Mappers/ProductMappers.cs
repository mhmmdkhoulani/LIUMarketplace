using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;

namespace LIUMarketPlace.Api.Mappers
{
    public static class ProductMappers
    {
        public static ProductDetailsDto ToProductDetailsDto(this Product product)
        {
            return new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                Desciption = product.Description,
                Price = product.Price,
                Status = product.Status,
                ImageCoverUrl = product.ImageCoverUrl,
                Category = product.Category.ToCategoryDto(),
            };
        } 

        public static Product ToProduct(this ProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Status = dto.Status,
                CategoryId = dto.CategoryId
            };
        }
    }
}
