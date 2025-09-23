namespace ProductManagement.DTOs
{
    public class ChatRoomDTO
    {
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int AdminId { get; set; }
        public string? AdminName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
