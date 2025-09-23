using AutoMapper;
using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandDTO> CreateAsync(BrandDTO dto)
        {
            var brand = _mapper.Map<Brand>(dto);
            await _brandRepository.CreateAsync(brand);
            return _mapper.Map<BrandDTO>(brand);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _brandRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            return brands.Select(b => _mapper.Map<BrandDTO>(b));
        }

        public async Task<BrandDTO?> GetByIdAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null) return null;
            return _mapper.Map<BrandDTO>(brand);
        }

        public async Task<bool> UpdateAsync(int id, BrandDTO dto)
        {
            var existing = await _brandRepository.GetByIdAsync(id);
            if (existing == null) return false;
            // map fields
            existing.Name = dto.Name;
            existing.CategoryId = dto.CategoryId;
            existing.ImageURL = dto.ImageURL;
            existing.UpdatedAt = DateTime.UtcNow;
            await _brandRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<ProductManagement.DTOs.BrandPagedResultDTO> GetPagedAsync(int pageNumber, int pageSize, string? search = null, int? categoryId = null)
        {
            var (items, total) = await _brandRepository.GetPagedAsync(pageNumber, pageSize, search, categoryId);
            var dtoItems = items.Select(b => _mapper.Map<BrandDTO>(b)).ToList();
            return new ProductManagement.DTOs.BrandPagedResultDTO
            {
                Items = dtoItems,
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<bool> UpdateImageAsync(int id, string imageUrl)
        {
            var existing = await _brandRepository.GetByIdAsync(id);
            if (existing == null) return false;
            existing.ImageURL = imageUrl;
            existing.UpdatedAt = DateTime.UtcNow;
            await _brandRepository.UpdateAsync(existing);
            return true;
        }
    }
}
