namespace ProductManagement.DTOs
{
    public class OrderCreateDTO
    {
        public int? UserId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public int? VoucherId { get; set; }
        public int OrderStatusId { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingDistrict { get; set; }
        public string? ShippingWard { get; set; }
        public int PaymentId { get; set; } 
        public List<OrderItemCreateDTO> OrderItems { get; set; } = new();
    }
}
