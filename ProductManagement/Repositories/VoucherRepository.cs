using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly AppDbContext _context;
        public VoucherRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Voucher>> GetAllVouchersAsync()
        {
            return await _context.Vouchers.ToListAsync();
        }
        public async Task<Voucher?> GetVoucherByIdAsync(int voucherId)
        {
            return await _context.Vouchers.FindAsync(voucherId);
        }
        public async Task<Voucher?> GetVoucherByCodeAsync(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(v => v.Code == code);
        }
        public async Task AddVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteVoucherAsync(int voucherId)
        {
            var voucher = await GetVoucherByIdAsync(voucherId);
            if (voucher != null)
            {
                _context.Vouchers.Remove(voucher);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsVoucherCodeUniqueAsync(string code, int? excludeVoucherId = null)
        {
            return !await _context.Vouchers.AnyAsync(v => v.Code == code && (!excludeVoucherId.HasValue || v.VoucherId != excludeVoucherId.Value));
        }
    }
}
