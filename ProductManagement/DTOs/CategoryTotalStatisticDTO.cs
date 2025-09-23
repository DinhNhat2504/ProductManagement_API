namespace ProductManagement.DTOs
{
    public class CategoryTotalStatisticDTO
    {
        public string CategoryName { get; set; }
        public int TotalQuantity { get; set; }
        

    }

    public class StatisticResultDTO<T>
    {
        public List<T> Details { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalReveneu { get; set; }
        public List<CategoryTotalStatisticDTO> CategoryTotals { get; set; } // Dùng cho Import, Export, Product
    }
}
