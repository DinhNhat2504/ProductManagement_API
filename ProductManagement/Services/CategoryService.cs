using AutoMapper;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
             await _categoryRepository.CreateCategoryAsync(category);
             return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return false; // Category not found
            }
            await _categoryRepository.DeleteCategoryAsync(id);
            return true;
        }

        public Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = _categoryRepository.GetAllCategoriesAsync();
            return categories;
        }

        public Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = _categoryRepository.GetCategoryByIdAsync(id);
            return category;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(category.CategoryId);
            if (existingCategory == null)
            {
                return false; // Category not found
            }
            await _categoryRepository.UpdateCategoryAsync(category);
            return true;
        }
    }
}
