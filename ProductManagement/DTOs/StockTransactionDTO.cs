namespace ProductManagement.DTOs
{
    public class StockTransactionDTO
    {
        public int ProductId { get; set; }
        public int QuantityChanged { get; set; }
        public string? Note { get; set; }
    }
}
