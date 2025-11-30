using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;
using API.Movies.Repository.IRepository;
using API.Movies.Services.IServices;
using AutoMapper;

namespace API.Movies.Services
{
    public class CategoryService(ICaregoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {
        private readonly ICaregoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryDto> AddCategoryAsync(CategoryCreateDto categoryDto)
        {
            var categoryExists = await _categoryRepository.CategoryExistsByNameAsync(categoryDto.Name);
            if (categoryExists)
            {
                throw new InvalidOperationException($"Category with name {categoryDto.Name} already exists");
            }

            var category = _mapper.Map<Category>(categoryDto);
            var wasCreated = await _categoryRepository.AddCategoryAsync(category);
            if (!wasCreated)
            {
                throw new Exception($"Category with name {categoryDto.Name} could not be created");
            }

            return _mapper.Map<CategoryDto>(category);
        }

        public Task<bool> CategoryExistsByIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CategoryExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            return _mapper.Map<ICollection<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryAsync(categoryId);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryCreateDto categoryDto)
        {
            var category = await _categoryRepository.GetCategoryAsync(categoryId);
            if (category == null)
            {
                throw new InvalidOperationException($"Category with id {categoryId} does not exist");
            }

            var nameExists = await _categoryRepository.CategoryExistsByNameAsync(categoryDto.Name);
            if (nameExists)
            {
                throw new InvalidOperationException($"Category with name {categoryDto.Name} already exists");
            }

            _mapper.Map(categoryDto, category);
            var wasUpdated = await _categoryRepository.UpdateCategoryAsync(category);
            if (!wasUpdated)
            {
                throw new Exception($"Category with id {categoryId} could not be updated");
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
}