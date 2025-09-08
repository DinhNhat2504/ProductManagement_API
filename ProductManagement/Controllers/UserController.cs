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
        [HttpPost("validate-token")]
        public IActionResult ValidateToken([FromBody] string token)
        {
            var principal = _jwtService.ValidateToken(token);
            if (principal == null)
                return Unauthorized("Token không hợp lệ hoặc đã hết hạn");

            // Có thể trả về thông tin user từ claim
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(new { userId });
        }
        // Lấy danh sách user phân trang (yêu cầu JWT)
        [Authorize]
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            var pagedResult = await _userService.GetPagedUserAsync(pageNumber, pageSize, searchTerm);
            return Ok(pagedResult);
        }
        // Thêm người dùng mới (yêu cầu JWT)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewUser([FromForm] UserDTO dto)
        {
            if (dto.AvatarImage != null && dto.AvatarImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.AvatarImage.FileName);
                var filePath = Path.Combine("wwwroot/images/users", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.AvatarImage.CopyToAsync(stream);
                }

                dto.AvatarUrl = "/images/products/" + fileName;
            }

            var user = await _userService.AddProductAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }
        // Lấy thông tin user theo id (yêu cầu JWT)
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        // Cập nhật thông tin user (yêu cầu JWT)
        [Authorize]
        [HttpPut("{id}")]   
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UserDTO dto)
        {
            if (id != dto.UserId)
                return BadRequest("User ID mismatch");
            // Lấy thông tin user cũ
            var oldUser = await _userService.GetByIdAsync(id);
            if (oldUser == null)
                return NotFound("User không tồn tại");
            // Nếu có file ảnh mới
            if (dto.AvatarImage != null && dto.AvatarImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.AvatarImage.FileName);
                var filePath = Path.Combine("wwwroot/images/users", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.AvatarImage.CopyToAsync(stream);
                }
                dto.AvatarUrl = "/images/users/" + fileName;
            }
            else
            {
                // Giữ nguyên URL ảnh cũ nếu không có ảnh mới
                dto.AvatarUrl = oldUser.AvatarUrl;
            }
            var success = await _userService.UpdateUserAsync(id, dto);
            if (!success)
                return StatusCode(500, "Cập nhật user thất bại");
            return Ok();
        }
        // Xóa user (yêu cầu JWT)
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound("User không tồn tại");
            return Ok();
        }
    }
}
