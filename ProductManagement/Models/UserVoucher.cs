using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
    public class UserVoucher
    {
        // Khóa ngoại đến User
        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public User User { get; set; }

        // Khóa ngoại đến Voucher
        [Required]
        [ForeignKey("Vouchers")]
        public int VoucherId { get; set; }
        public Voucher Voucher { get; set; }
    }
}
