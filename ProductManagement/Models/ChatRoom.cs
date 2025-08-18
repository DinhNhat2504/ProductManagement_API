using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class ChatRoom
{
    [Key]
    public int ChatRoomId { get; set; }

    // Người khởi tạo cuộc trò chuyện (ví dụ: khách hàng)
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }

    // Admin tham gia cuộc trò chuyện
    [Required]
    [ForeignKey("Admin")]
    public int AdminId { get; set; }
    public User? Admin { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}