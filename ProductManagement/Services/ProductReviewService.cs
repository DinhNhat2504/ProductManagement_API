using ProductManagement.Models;
using ProductManagement.DTOs;
using AutoMapper;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepository _repository;
        private readonly IMapper _mapper;
        public ProductReviewService(IProductReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddProductReviewAsync(ProductReviewDTO dto)
        {
            var review = _mapper.Map<ProductReview>(dto);
            review.CreatedAt = DateTime.Now;
            review.UpdatedAt = DateTime.Now;
            await _repository.AddProductReviewAsync(review);
        }
        public async Task DeleteProductReviewAsync(int reviewId)
        {
            await _repository.DeleteProductReviewAsync(reviewId);
        }
        public async Task<ProductReviewDTO?> GetReviewByIdAsync(int reviewId)
        {
            var review = await _repository.GetReviewByIdAsync(reviewId);
            if(review == null) throw new Exception("Review not found");
            return _mapper.Map<ProductReviewDTO>(review);

        }
        
        public async Task UpdateProductReviewAsync(int reviewId, ProductReviewDTO dto)
        {
            var review = await _repository.GetReviewByIdAsync(reviewId);
            if(review == null) throw new Exception("Review not found");
            _mapper.Map(dto, review);
            review.UpdatedAt = DateTime.Now;
            await _repository.UpdateProductReviewAsync(review);
        }
    }
}
