using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Category;
using Data.Data;
using Domain.Models.Customer;
using Infrastructure.Contracts;

namespace Infrastructure.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryViewDto>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(category => new CategoryViewDto
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description
                })
                .ToListAsync();
        }

        public async Task<CategoryViewDto> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryViewDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryViewDto> CreateCategoryAsync(CategoryInputDto categoryInputModel)
        {
            var category = new Data.Models.Category
            {
                Name = categoryInputModel.Name,
                Description = categoryInputModel.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryViewDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryViewDto> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateModel)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null) return null;

            existingCategory.Name = categoryUpdateModel.Name;
            existingCategory.Description = categoryUpdateModel.Description;

            await _context.SaveChangesAsync();

            return new CategoryViewDto
            {
                CategoryId = existingCategory.CategoryId,
                Name = existingCategory.Name,
                Description = existingCategory.Description
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
