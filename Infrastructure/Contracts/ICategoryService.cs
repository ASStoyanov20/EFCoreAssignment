using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using Domain.Models.Category;

namespace Infrastructure.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewDto>> GetAllCategoriesAsync();
    Task<CategoryViewDto> GetCategoryByIdAsync(int id);
    Task<CategoryViewDto> CreateCategoryAsync(CategoryInputDto categoryInputModel);
    Task<CategoryViewDto> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateModel);
    Task<bool> DeleteCategoryAsync(int id);
}