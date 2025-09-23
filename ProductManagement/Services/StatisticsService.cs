using ProductManagement.DTOs;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;
        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }
        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetUserStatisticsAsync(DateTime? fromDate, DateTime? toDate)
        {
            var to = toDate ?? DateTime.Today;
            var from = fromDate ?? to.AddDays(-30);
            return await _statisticsRepository.GetUserStatisticsAsync(from, to);
        }
        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetProductStatisticsAsync(DateTime? fromDate, DateTime? toDate)
        {
            var to = toDate ?? DateTime.Today;
            var from = fromDate ?? to.AddDays(-30);
            return await _statisticsRepository.GetProductStatisticsAsync(from, to);
        }
        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetImportStockStatisticsAsync(DateTime? fromDate, DateTime? toDate)
        {
            var to = toDate ?? DateTime.Today;
            var from = fromDate ?? to.AddDays(-30);
            return await _statisticsRepository.GetImportStockStatisticsAsync(from, to);
        }
        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetExportStockStatisticsAsync(DateTime? fromDate, DateTime? toDate)
        {
            var to = toDate ?? DateTime.Today;
            var from = fromDate ?? to.AddDays(-30);
            return await _statisticsRepository.GetExportStockStatisticsAsync(from, to);
        }
        public async Task<StatisticResultDTO<MonthlyStatisticDTO>> GetOrderStatisticsAsync(DateTime? fromDate, DateTime? toDate)
        {
            var to = toDate ?? DateTime.Today;
            var from = fromDate ?? to.AddDays(-30);
            return await _statisticsRepository.GetOrderStatisticsAsync(from, to);
        }
        public async Task<StatisticResultDTO<DailyRevenueDTO>> GetRevenueStatisticsAsync(DateTime? fromDate, DateTime? toDate)
        {
            var to = toDate ?? DateTime.Today;
            var from = fromDate ?? to.AddDays(-30);
            return await _statisticsRepository.GetRevenueStatisticsAsync(from, to);
        }
    }
}
