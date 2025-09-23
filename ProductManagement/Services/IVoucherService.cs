using ProductManagement.DTOs;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IVoucherService
    {
        Task<IEnumerable<VoucherDTO>> GetAllVouchersAsync();
        Task<VoucherDTO?> GetVoucherByIdAsync(int voucherId);
        Task<VoucherDTO?> GetVoucherByCodeAsync(string code);
        Task AddVoucherAsync(VoucherDTO voucher);
        Task<bool> UpdateVoucherAsync(VoucherDTO voucher);
        Task<bool> DeleteVoucherAsync(int voucherId);
        Task<bool> IsVoucherCodeUniqueAsync(string code, int? excludeVoucherId = null);
    }
}
