using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.DTOs.Auth;
using CffHackathon.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CffHackathon.Application.Services
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthService(
            UserManager<AppUser> userManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(string email, string password)
        {
            var user = new AppUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);

            await _userManager.AddToRoleAsync(user, "Customer");
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User tapılmadı");

            if (!await _userManager.CheckPasswordAsync(user, password))
                throw new Exception("Şifrə yanlışdır");

            return await _jwtService.GenerateToken(user);
        }

        public async Task<string> AssignedRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);
            return "Role assigned successfully";
        }

        public async Task<List<UserDto>> GetUsersByRoleAsync(string roleName)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

            var userDtos = new List<UserDto>();

            foreach (var user in usersInRole)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Roles = roles.ToList()
                });
            }

            return userDtos;
        }
    }

}



