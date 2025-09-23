using System.ComponentModel.DataAnnotations;

namespace ProductManagement.DTOs
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100)]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(20)]
        public string CustomerPhone { get; set; } = string.Empty;

        public int? VoucherId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn 0")]
        public decimal TotalPrice { get; set; }

        [Required]
        public int OrderStatusId { get; set; }

        [Required]
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Địa chỉ giao hàng không được để trống")]
        [StringLength(200)]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ShippingProvince { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ShippingDistrict { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ShippingWard { get; set; } = string.Empty;

        public string OrderStatus { get; set; } = string.Empty;
        public string? PaymentName { get; set; }
        public DateTime? CreatedAt { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Đơn hàng phải có ít nhất 1 sản phẩm")]
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
