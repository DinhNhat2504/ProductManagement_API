using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<IEnumerable<User>> SearchUserAsync(string searchTerm);
    }
}
