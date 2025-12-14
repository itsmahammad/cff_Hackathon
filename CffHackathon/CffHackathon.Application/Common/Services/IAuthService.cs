using CffHackathon.Application.DTOs.Auth;

namespace CffHackathon.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(string email, string password);
        Task<string> LoginAsync(string email, string password);
        Task<string> AssignedRole(string userId, string roleName);
        Task<List<UserDto>> GetUsersByRoleAsync(string roleName);
    }
}
