using ProductManagement.DTOs;

namespace ProductManagement.Repositories
{
    public interface IStatisticsRepository
    {
        Task<StatisticResultDTO<MonthlyStatisticDTO>> GetUserStatisticsAsync(DateTime fromDate, DateTime toDate);
        Task<StatisticResultDTO<MonthlyStatisticDTO>> GetProductStatisticsAsync(DateTime fromDate, DateTime toDate);
        Task<StatisticResultDTO<MonthlyStatisticDTO>> GetImportStockStatisticsAsync(DateTime fromDate, DateTime toDate);
        Task<StatisticResultDTO<MonthlyStatisticDTO>> GetExportStockStatisticsAsync(DateTime fromDate, DateTime toDate);
        Task<StatisticResultDTO<MonthlyStatisticDTO>> GetOrderStatisticsAsync(DateTime fromDate, DateTime toDate);
        Task<StatisticResultDTO<DailyRevenueDTO>> GetRevenueStatisticsAsync(DateTime fromDate, DateTime toDate);

    }
}
