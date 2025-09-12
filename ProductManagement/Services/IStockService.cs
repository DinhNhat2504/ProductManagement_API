using ProductManagement.DTOs;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IStockService
    {
        Task ImportStockAsync(StockTransactionDTO dto);
        Task<bool> ExportStockAsync(StockTransactionDTO dto);
        Task<IEnumerable<StockTransaction>> GetTransactionsAsync(int productId);
        Task<Stock?> GetStockByProductIdAsync(int productId);
    }
}
