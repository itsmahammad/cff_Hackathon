using CffHackathon.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

}

