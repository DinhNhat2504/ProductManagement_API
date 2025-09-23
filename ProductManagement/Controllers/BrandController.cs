using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProductManagement.DTOs;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null, [FromQuery] int? categoryId = null)
        {
            // If paging params provided, return paged result; otherwise return all
            if (pageNumber > 0 && pageSize > 0)
            {
                var paged = await _brandService.GetPagedAsync(pageNumber, pageSize, search, categoryId);
                return Ok(paged);
            }

            var brands = await _brandService.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound();
            return Ok(brand);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] BrandDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _brandService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BrandId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] BrandDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _brandService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _brandService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}/upload-image")]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ProductManagement.DTOs.BrandImageResponseDTO), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsDir = Path.Combine("wwwroot", "images", "brands");
            if (!Directory.Exists(uploadsDir)) Directory.CreateDirectory(uploadsDir);

            var existing = await _brandService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // delete old image if exists
            if (!string.IsNullOrEmpty(existing.ImageURL))
            {
                var oldPath = Path.Combine("wwwroot", existing.ImageURL.TrimStart('/'));
                if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsDir, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = "/images/brands/" + fileName;
            var updated = await _brandService.UpdateImageAsync(id, imageUrl);
            if (!updated) return NotFound();

            var response = new ProductManagement.DTOs.BrandImageResponseDTO { ImageURL = imageUrl };
            return Ok(response);
        }
    }
}
