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
           return  await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
