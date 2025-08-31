namespace ProductManagement.DTOs
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public Double Price { get; set; }
    }
}
