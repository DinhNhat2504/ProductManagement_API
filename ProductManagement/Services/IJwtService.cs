using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
