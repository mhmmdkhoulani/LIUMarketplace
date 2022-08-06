using LIUMarketplace.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.UI.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> AddCategoryAsync(CategoryDto dto);
        Task<CategoryDto> UpdateCategoryAsync(CategoryDto dto);
        Task DeleteCategoryAsync(int id);
        Task<CategoryDto> GetCategoryByIdAsync(int id);

    }
}
