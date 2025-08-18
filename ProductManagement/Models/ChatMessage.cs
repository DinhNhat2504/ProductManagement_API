using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models;

public class ChatMessage
{
    [Key]
    public int MessageId { get; set; }

    [Required]
    [ForeignKey("ChatRoom")]
    public int ChatRoomId { get; set; }
    public ChatRoom? ChatRoom { get; set; }

    // Người gửi tin nhắn (admin hoặc khách hàng)
    [Required]
    [ForeignKey("User")]
    public int SenderId { get; set; }
    public User? Sender { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime SentAt { get; set; }
}