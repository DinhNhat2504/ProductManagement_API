namespace ProductManagement.DTOs
{
    public class StockResponseDTO
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
