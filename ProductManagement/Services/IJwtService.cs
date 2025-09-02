using ProductManagement.Models;
using System.Security.Claims;

namespace ProductManagement.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
