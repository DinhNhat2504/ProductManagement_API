using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockByProductId(int productId);
        Task ImportStock(int productId, int quantity, string? note = null);
        Task<bool> ExportStock(int productId, int quantity, string? note = null);
        Task<IEnumerable<StockTransaction>> GetTransactions(int productId);
    }
}
