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
            

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);
        }

    }
}
