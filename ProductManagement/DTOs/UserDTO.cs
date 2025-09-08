namespace ProductManagement.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? AvatarUrl { get; set; }
        public IFormFile? AvatarImage { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
