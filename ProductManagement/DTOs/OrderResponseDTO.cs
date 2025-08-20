namespace ProductManagement.DTOs
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public int? VoucherId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingDistrict { get; set; }
        public string? ShippingWard { get; set; }
        public List<OrderItemResponseDTO> OrderItems { get; set; } = new();
    }
}
