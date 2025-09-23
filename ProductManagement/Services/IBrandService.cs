using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDTO>> GetAllAsync();
        Task<BrandDTO?> GetByIdAsync(int id);
        Task<BrandDTO> CreateAsync(BrandDTO dto);
        Task<bool> UpdateAsync(int id, BrandDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<ProductManagement.DTOs.BrandPagedResultDTO> GetPagedAsync(int pageNumber, int pageSize, string? search = null, int? categoryId = null);
        Task<bool> UpdateImageAsync(int id, string imageUrl);
    }
}
