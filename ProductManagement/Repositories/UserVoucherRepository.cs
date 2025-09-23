using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class UserVoucherRepository : IUserVoucherRepository
    {
        private readonly AppDbContext _context;
        public UserVoucherRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserVoucher?>> GetAllUserVoucherAsync()
        {
            return await _context.UserVouchers.ToListAsync();
        }
        public async Task<UserVoucher?> GetUserVoucherAsync(int userId, int voucherId)
        {
            return await _context.UserVouchers
                .FirstOrDefaultAsync(uv => uv.UserId == userId && uv.VoucherId == voucherId);
        }
        public async Task AddUserVoucherAsync(UserVoucher userVoucher)
        {
            _context.UserVouchers.Add(userVoucher);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserVoucherAsync(UserVoucher userVoucher)
        {
            _context.UserVouchers.Update(userVoucher);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserVoucherAsync(UserVoucher userVoucher)
        {
            _context.UserVouchers.Remove(userVoucher);
            await _context.SaveChangesAsync();
        }
    }
}
