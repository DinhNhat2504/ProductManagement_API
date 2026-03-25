namespace ProductManagement.DTOs
{
    public class CategoryTotalStatisticDTO
    {
        public string CategoryName { get; set; } = string.Empty;
        public int TotalQuantity { get; set; }
        

    }

    public class StatisticResultDTO<T>
    {
        public int TotalQuantity { get; set; }
        public decimal TotalReveneu { get; set; }
    }
}
