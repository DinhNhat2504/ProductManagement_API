using AutoMapper;
using ProductManagement.Data;
using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> AddProductAsync(UserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            await _userRepository.AddAsync(user);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);
            if (existingUser == null) return false;
            await _userRepository.DeleteAsync(existingUser);
            return true;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUserAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public async Task<User?> GetEntityByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<PagedResult<UserDTO>> GetPagedUserAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            var allProducts = string.IsNullOrWhiteSpace(searchTerm)
                 ? await _userRepository.GetAllUserAsync()
                 : await _userRepository.SearchUserAsync(searchTerm);
            var totalItems = allProducts.Count();
            var items = allProducts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => _mapper.Map<UserDTO>(p))
                .ToList();
            return new PagedResult<UserDTO>
            {
                Items = items,
                TotalCount = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<UserDTO?> LoginAsync(UserLoginDTO dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.HashedPassword))
                return null;

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO?> RegisterAsync(UserRegisterDTO dto)
        {
            if (await _userRepository.GetByEmailAsync(dto.Email) != null)
                return null;

            var user = _mapper.Map<User>(dto);
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.RoleId = 2; 

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> UpdateUserAsync(int userId, UserDTO dto)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);
            if (existingUser == null) return false;
            _mapper.Map(dto, existingUser);
            existingUser.UpdatedAt = DateTime.Now;
            await _userRepository.UpdateAsync(existingUser);
            return true;
        }
    }
}
