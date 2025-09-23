using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand?> GetByIdAsync(int id);
        Task CreateAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task<bool> DeleteAsync(int id);
        Task<(IEnumerable<Brand> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string? search = null, int? categoryId = null);
    }
}
