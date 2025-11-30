using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;

namespace API.Movies.Services.IServices
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto?> GetCategoryAsync(int categoryId);
        Task<bool> CategoryExistsByIdAsync(int categoryId);
        Task<bool> CategoryExistsByNameAsync(string name);
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}