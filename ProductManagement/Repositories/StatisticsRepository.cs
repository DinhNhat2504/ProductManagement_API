using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.DTOs;
using System.Text.RegularExpressions;

namespace ProductManagement.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly AppDbContext _context;
        public StatisticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetExportStockStatisticsAsync(DateTime fromDate, DateTime toDate)
        {
            var detailsRaw = await (from st in _context.StockTransactions
                                    join p in _context.Products on st.ProductId equals p.ProductId
                                    join c in _context.Categories on p.CategoryId equals c.CategoryId
                                    where !st.IsImport && st.TransactionDate >= fromDate && st.TransactionDate <= toDate
                                    group st by new { Date = st.TransactionDate.Date, CategoryName = c.Name } into g
                                    orderby g.Key.Date, g.Key.CategoryName
                                    select new
                                    {
                                        Date = g.Key.Date,
                                        CategoryName = g.Key.CategoryName,
                                        Value = g.Sum(x => x.QuantityChanged)
                                    })
                                   .ToListAsync();

            var details = detailsRaw
                .Select(x => new MonthlyStatisticDTO
                {
                    Date = x.Date.ToString("yyyy-MM-dd"),
                    CategoryName = x.CategoryName,
                    Value = x.Value
                })
                .ToList();

            var categoryTotals = await (from st in _context.StockTransactions
                                        join p in _context.Products on st.ProductId equals p.ProductId
                                        join c in _context.Categories on p.CategoryId equals c.CategoryId
                                        where !st.IsImport && st.TransactionDate >= fromDate && st.TransactionDate <= toDate
                                        group st by c.Name into g
                                        select new CategoryTotalStatisticDTO
                                        {
                                            CategoryName = g.Key,
                                            TotalQuantity = g.Sum(x => x.QuantityChanged) 
                                        })
                                      .ToListAsync();

            var totalQuantity = categoryTotals.Sum(x => x.TotalQuantity);

            return new StatisticResultDTO<MonthlyStatisticDTO>
            {
                Details = details,
                TotalQuantity = totalQuantity,
                CategoryTotals = categoryTotals
            };
        }

        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetImportStockStatisticsAsync(DateTime fromDate, DateTime toDate)
        {
            var detailsRaw = await (from st in _context.StockTransactions
                                    join p in _context.Products on st.ProductId equals p.ProductId
                                    join c in _context.Categories on p.CategoryId equals c.CategoryId
                                    where st.IsImport && st.TransactionDate >= fromDate && st.TransactionDate <= toDate
                                    group st by new { Date = st.TransactionDate.Date, CategoryName = c.Name } into g
                                    orderby g.Key.Date, g.Key.CategoryName
                                    select new
                                    {
                                        Date = g.Key.Date,
                                        CategoryName = g.Key.CategoryName,
                                        Value = g.Sum(x => x.QuantityChanged)
                                    })
                                   .ToListAsync();

            var details = detailsRaw
                .Select(x => new MonthlyStatisticDTO
                {
                    Date = x.Date.ToString("yyyy-MM-dd"),
                    CategoryName = x.CategoryName,
                    Value = x.Value
                })
                .ToList();

            var categoryTotals = await (from st in _context.StockTransactions
                                        join p in _context.Products on st.ProductId equals p.ProductId
                                        join c in _context.Categories on p.CategoryId equals c.CategoryId
                                        where st.IsImport && st.TransactionDate >= fromDate && st.TransactionDate <= toDate
                                        group st by c.Name into g
                                        select new CategoryTotalStatisticDTO
                                        {
                                            CategoryName = g.Key,
                                            TotalQuantity = g.Sum(x => x.QuantityChanged)
                                        })
                                      .ToListAsync();

            var totalQuantity = categoryTotals.Sum(x => x.TotalQuantity);

            return new StatisticResultDTO<MonthlyStatisticDTO>
            {
                Details = details,
                TotalQuantity = totalQuantity,
                CategoryTotals = categoryTotals
            };
        }

        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetOrderStatisticsAsync(DateTime fromDate, DateTime toDate)
        {
            var grouped = await _context.Orders
                .Where(o => o.CreatedAt >= fromDate && o.CreatedAt <= toDate)
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Value = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            var details = grouped
                .Select(x => new MonthlyStatisticDTO
                {
                    Date = x.Date.ToString("yyyy-MM-dd"),
                    Value = x.Value
                })
                .ToList();

            var totalQuantity = details.Sum(x => x.Value);

            return new StatisticResultDTO<MonthlyStatisticDTO>
            {
                Details = details,
                TotalQuantity = totalQuantity
            };
        }

        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetProductStatisticsAsync(DateTime fromDate, DateTime toDate)
        {
            // Lấy danh sách chi tiết theo ngày và danh mục
            var detailsRaw = await (from p in _context.Products
                                    join c in _context.Categories on p.CategoryId equals c.CategoryId
                                    where p.CreatedAt >= fromDate && p.CreatedAt <= toDate
                                    group p by new { Date = p.CreatedAt.Date, CategoryName = c.Name } into g
                                    orderby g.Key.Date, g.Key.CategoryName
                                    select new
                                    {
                                        Date = g.Key.Date,
                                        CategoryName = g.Key.CategoryName,
                                        Value = g.Count()
                                    })
                                   .ToListAsync();

            var details = detailsRaw
                .Select(x => new MonthlyStatisticDTO
                {
                    Date = x.Date.ToString("yyyy-MM-dd"),
                    CategoryName = x.CategoryName,
                    Value = x.Value
                })
                .ToList();

            // Tổng số lượng theo từng danh mục
            var categoryTotals = await (from p in _context.Products
                                        join c in _context.Categories on p.CategoryId equals c.CategoryId
                                        where p.CreatedAt >= fromDate && p.CreatedAt <= toDate
                                        group p by c.Name into g
                                        select new CategoryTotalStatisticDTO
                                        {
                                            CategoryName = g.Key,
                                            TotalQuantity = g.Count()
                                        })
                                       .ToListAsync();

            // Tổng số lượng tất cả sản phẩm
            var totalQuantity = categoryTotals.Sum(x => x.TotalQuantity);

            return new StatisticResultDTO<MonthlyStatisticDTO>
            {
                Details = details,
                TotalQuantity = totalQuantity,
                CategoryTotals = categoryTotals
            };
        }

        public async Task<StatisticResultDTO<DailyRevenueDTO>> GetRevenueStatisticsAsync(DateTime fromDate, DateTime toDate)
        {
            var grouped = await _context.Orders
                .Where(o => o.CreatedAt >= fromDate && o.CreatedAt <= toDate)
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.TotalPrice)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            var details = grouped
                .Select(x => new DailyRevenueDTO
                {
                    Date = x.Date.ToString("yyyy-MM-dd"),
                    Revenue = x.Revenue
                })
                .ToList();

            var totalQuantity = details.Sum(x => x.Revenue);

            return new StatisticResultDTO<DailyRevenueDTO>
            {
                Details = details,
                TotalReveneu = totalQuantity
            };
        }
        

        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetUserStatisticsAsync(DateTime fromDate, DateTime toDate)
        {
            var grouped = await _context.Users
                .Where(o => o.RoleId != 1 && o.CreatedAt >= fromDate && o.CreatedAt <= toDate)
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Value = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            var details = grouped
                .Select(x => new MonthlyStatisticDTO
                {
                    Date = x.Date.ToString("yyyy-MM-dd"),
                    Value = x.Value
                })
                .ToList();

            var totalQuantity = details.Sum(x => x.Value);

            return new StatisticResultDTO<MonthlyStatisticDTO>
            {
                Details = details,
                TotalQuantity = totalQuantity
            };
        }
    }
}
