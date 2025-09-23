namespace ProductManagement.DTOs
{
    public class StockTransactionResponseDTO
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int QuantityChanged { get; set; }
        public bool IsImport { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Note { get; set; }
    }
}
