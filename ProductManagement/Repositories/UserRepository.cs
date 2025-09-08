using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser != null)
            {
                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _context.Users.Include(u => u.Role)
                                       .ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Role)
                 .FirstOrDefaultAsync(u => u.UserId == id);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> SearchUserAsync(string searchTerm)
        {
            return await _context.Users
                 .Include(r => r.Role)
                 .Where(n => n.FirstName!.Contains(searchTerm) || n.LastName!.Contains(searchTerm) || n.Email!.Contains(searchTerm)).ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
