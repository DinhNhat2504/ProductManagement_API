using ProductManagement.DTOs;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IUserService
    {
        Task<UserDTO?> RegisterAsync(UserRegisterDTO dto);
        Task<UserDTO?> LoginAsync(UserLoginDTO dto);
        Task<UserDTO?> GetByIdAsync(int id);
        Task<User?> GetEntityByEmailAsync(string email);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}
