using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _dbContext;

        public StockRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ImportStock(int productId, int quantity, string? note = null)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (stock == null)
            {
                stock = new Stock
                {
                    ProductId = productId,
                    Quantity = quantity,
                    LastUpdated = DateTime.UtcNow
                };
                await _dbContext.Stocks.AddAsync(stock);
            }
            else
            {
                stock.Quantity += quantity;
                stock.LastUpdated = DateTime.UtcNow;
            }

            await _dbContext.StockTransactions.AddAsync(new StockTransaction
            {
                ProductId = productId,
                QuantityChanged = quantity,
                TransactionDate = DateTime.UtcNow,
                Note = note
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExportStock(int productId, int quantity, string? note = null)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (stock == null || stock.Quantity < quantity)
                return false;

            stock.Quantity -= quantity;
            stock.LastUpdated = DateTime.UtcNow;

            await _dbContext.StockTransactions.AddAsync(new StockTransaction
            {
                ProductId = productId,
                QuantityChanged = -quantity,
                TransactionDate = DateTime.UtcNow,
                Note = note
            });

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Stock?> GetStockByProductId(int productId)
        {
            return await _dbContext.Stocks
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.ProductId == productId);
        }

        public async Task<IEnumerable<StockTransaction>> GetTransactions(int productId)
        {
            return await _dbContext.StockTransactions
                .Where(t => t.ProductId == productId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
    }
}