using LIUMarketplace.Models;
using LIUMarketplace.Models.Models;
using LIUMarketplace.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LIUMarketPlace.Api.Services
{
    public interface ICategoryService
    {
        Task<Category> AddCategoryAsync(CategoryDto model);
        Task<Category> UpdateCategoryAsync(Category model);
        Task DeleteCategoryAsync(int id);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Category> AddCategoryAsync(CategoryDto model)
        {
            var category = new Category
            {
                Name = model.Name,
            }; 
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();   
            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);

            if(category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _db.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);

            if(category != null)
            {
                return category;
            }
            return null;
        }

        public async Task<Category> UpdateCategoryAsync(Category model)
        {
            var category = await _db.Categories.FindAsync(model.Id);

            if(category != null)
            {
                category.Name = model.Name;
                await _db.SaveChangesAsync();
                return category;
            }

            return null;
        }
    }
}
