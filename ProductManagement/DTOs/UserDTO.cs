namespace ProductManagement.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? AvatarUrl { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
