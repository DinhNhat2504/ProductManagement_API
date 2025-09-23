using AutoMapper;
using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;
        public VoucherService(IVoucherRepository voucherRepository, IMapper mapper)
        {
            _voucherRepository = voucherRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<VoucherDTO>> GetAllVouchersAsync()
        {
            var vouchers = await _voucherRepository.GetAllVouchersAsync();
            return _mapper.Map<IEnumerable<VoucherDTO>>(vouchers);
        }
        public async Task<VoucherDTO?> GetVoucherByIdAsync(int voucherId)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);
            return _mapper.Map<VoucherDTO?>(voucher);
        }
        public async Task<VoucherDTO?> GetVoucherByCodeAsync(string code)
        {
            var voucher = await _voucherRepository.GetVoucherByCodeAsync(code);
            return _mapper.Map<VoucherDTO?>(voucher);
        }
        public async Task AddVoucherAsync(VoucherDTO voucher)
        {
            var isUnique = await _voucherRepository.IsVoucherCodeUniqueAsync(voucher.Code);
            if (!isUnique)
            {
                throw new ArgumentException("Voucher code must be unique.", nameof(voucher.Code));
            }
            if (voucher.DiscountType == "%")
            {
                if (voucher.DiscountValue < 0 || voucher.DiscountValue > 100)
                {
                    throw new ArgumentException("Percentage discount value must be between 0 and 100.", nameof(voucher.DiscountValue));
                }
                await _voucherRepository.AddVoucherAsync(_mapper.Map<Voucher>(voucher));
            }
            else if (voucher.DiscountType == "₫")
            {
                if (voucher.DiscountValue < 0)
                {
                    throw new ArgumentException("Fixed amount discount value must be non-negative.", nameof(voucher.DiscountValue));
                }
                await _voucherRepository.AddVoucherAsync(_mapper.Map<Voucher>(voucher));
            }
            else
            {
                throw new ArgumentException("Invalid discount type. Must be '%' or '₫'.", nameof(voucher.DiscountType));
            }
        }
        public async Task<bool> UpdateVoucherAsync(VoucherDTO voucher)
        {
            var isUnique = await _voucherRepository.IsVoucherCodeUniqueAsync(voucher.Code, voucher.VoucherId);
            var existingVoucher = await _voucherRepository.GetVoucherByIdAsync(voucher.VoucherId);
            if (existingVoucher == null || !isUnique)
            {
                return false;
            }
            if (voucher.DiscountType == "%")
            {
                if (voucher.DiscountValue < 0 || voucher.DiscountValue > 100)
                {
                    throw new ArgumentException("Percentage discount value must be between 0 and 100.", nameof(voucher.DiscountValue));
                }
                await _voucherRepository.UpdateVoucherAsync(_mapper.Map<Voucher>(voucher));
                return true;
            }
            else if (voucher.DiscountType == "₫")
            {
                if (voucher.DiscountValue < 0)
                {
                    throw new ArgumentException("Fixed amount discount value must be non-negative.", nameof(voucher.DiscountValue));
                }
                await _voucherRepository.UpdateVoucherAsync(_mapper.Map<Voucher>(voucher));
                return true;
            }
            else
            {
                throw new ArgumentException("Invalid discount type. Must be '%' or '₫'.", nameof(voucher.DiscountType));
            }
            
        }
        public async Task<bool> DeleteVoucherAsync(int voucherId)
        {
            var existingVoucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);
            if (existingVoucher == null)
            {
                return false;
            }
            await _voucherRepository.DeleteVoucherAsync(voucherId);
            return true;
        }
        public async Task<bool> IsVoucherCodeUniqueAsync(string code, int? excludeVoucherId = null)
        {
            return await _voucherRepository.IsVoucherCodeUniqueAsync(code, excludeVoucherId);
        }
    }
}
