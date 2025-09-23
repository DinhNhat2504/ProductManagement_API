using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IUserVoucherRepository
    {
        Task<IEnumerable<UserVoucher?>> GetAllUserVoucherAsync();
        Task<UserVoucher?> GetUserVoucherAsync(int userId, int voucherId);
        Task AddUserVoucherAsync(UserVoucher userVoucher);
        Task UpdateUserVoucherAsync(UserVoucher userVoucher);
        Task DeleteUserVoucherAsync(UserVoucher userVoucher);
        
    }
}
