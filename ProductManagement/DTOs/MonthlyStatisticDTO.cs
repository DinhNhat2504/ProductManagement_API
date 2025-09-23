namespace ProductManagement.DTOs
{
    public class MonthlyStatisticDTO
    {
        public string Date { get; set; } = string.Empty; // yyyy-MM-dd
        public string? CategoryName { get; set; }
        public int Value { get; set; }
    }
}
