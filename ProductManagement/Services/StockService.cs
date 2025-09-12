using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task ImportStockAsync(StockTransactionDTO dto)
        {
            await _stockRepository.ImportStock(dto.ProductId, dto.QuantityChanged, dto.Note);
        }

        public async Task<bool> ExportStockAsync(StockTransactionDTO dto)
        {
            return await _stockRepository.ExportStock(dto.ProductId, dto.QuantityChanged, dto.Note);
        }

        public async Task<IEnumerable<StockTransaction>> GetTransactionsAsync(int productId)
        {
            return await _stockRepository.GetTransactions(productId);
        }

        public async Task<Stock?> GetStockByProductIdAsync(int productId)
        {
            return await _stockRepository.GetStockByProductId(productId);
        }
    }
}
