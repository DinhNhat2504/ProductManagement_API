using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs;
using ProductManagement.Services;
using System.Security.Claims;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // Đăng ký
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO dto)
        {
            var user = await _userService.RegisterAsync(dto);
            if (user == null)
                return BadRequest("Email đã tồn tại");

            return Ok(user);
        }

        // Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var user = await _userService.LoginAsync(dto);
            if (user == null)
                return Unauthorized("Email hoặc mật khẩu không đúng");

            // Lấy user entity để tạo JWT
            var userEntity = await _userService.GetEntityByEmailAsync(dto.Email);
            var token = _jwtService.GenerateToken(userEntity!);

            return Ok(new { token, user });
        }

        // Lấy thông tin user hiện tại (yêu cầu JWT)
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        // Lấy tất cả thông tin user  (yêu cầu JWT)
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
