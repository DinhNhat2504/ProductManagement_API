using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var products = await _categoryService.GetAllCategoriesAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "Internal server error: " + ex);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                    return NotFound();
                return Ok(category);
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "Internal server error: " + ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Category cannot be null");
            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, createdCategory);
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "Internal server error: " + ex);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null || id != category.CategoryId)
                return BadRequest("Category ID mismatch or null category");
            try
            {
                var result = await _categoryService.UpdateCategoryAsync(category);
                if (!result)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "Internal server error: " + ex);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (!result)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "Internal server error: " + ex);
            }
        }
    }
}
