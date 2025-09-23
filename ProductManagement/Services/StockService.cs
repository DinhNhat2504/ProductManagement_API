using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;
using System.Linq;

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

        public async Task<IEnumerable<StockTransactionResponseDTO>> GetTransactionsAsync(int productId)
        {
            var transactions = await _stockRepository.GetTransactions(productId);
            return transactions.Select(t => new StockTransactionResponseDTO
            {
                TransactionId = t.TransactionId,
                ProductId = t.ProductId,
                QuantityChanged = t.QuantityChanged,
                IsImport = t.IsImport,
                TransactionDate = t.TransactionDate,
                Note = t.Note
            }).ToList();
        }

        public async Task<StockResponseDTO?> GetStockByProductIdAsync(int productId)
        {
            var stock = await _stockRepository.GetStockByProductId(productId);
            if (stock == null) return null;
            return new StockResponseDTO
            {
                StockId = stock.StockId,
                ProductId = stock.ProductId,
                ProductName = stock.Product?.Name,
                Quantity = stock.Quantity,
                LastUpdated = stock.LastUpdated
            };
        }
    }
}
