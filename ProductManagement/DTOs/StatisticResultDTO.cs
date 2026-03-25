using System.Collections.Generic;

using System.Collections.Generic;

namespace ProductManagement.DTOs
{
    // Renamed to avoid a name collision that appeared during build.
    public class StatisticResult<T>
    {
        // List of detailed items (e.g. MonthlyStatisticDTO or DailyRevenueDTO)
        public List<T> Details { get; set; } = new List<T>();

        // Total count/quantity when applicable
        public int TotalQuantity { get; set; }

        // Total revenue when applicable (note: repository uses the property name "TotalReveneu")
        public decimal TotalReveneu { get; set; }

        // Category totals for statistics grouped by category
        public List<CategoryTotalStatisticDTO> CategoryTotals { get; set; } = new List<CategoryTotalStatisticDTO>();
    }
}
