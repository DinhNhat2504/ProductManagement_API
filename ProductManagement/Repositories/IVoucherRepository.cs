using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetAllVouchersAsync();
        Task<Voucher?> GetVoucherByIdAsync(int voucherId);
        Task<Voucher?> GetVoucherByCodeAsync(string code);
        Task AddVoucherAsync(Voucher voucher);
        Task UpdateVoucherAsync(Voucher voucher);
        Task DeleteVoucherAsync(int voucherId);
        Task<bool> IsVoucherCodeUniqueAsync(string code, int? excludeVoucherId = null);
    }
}
