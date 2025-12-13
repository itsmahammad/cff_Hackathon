using CffHackathon.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService
{
    private readonly IConfiguration _config;
    private readonly UserManager<AppUser> _userManager;
    public JwtService(IConfiguration config, UserManager<AppUser> userManager)
    {
        _config = config;
        _userManager = userManager;
    }

    public async Task<string> GenerateToken(AppUser user)
    {
        var keyString = _config["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(keyString))
            throw new Exception("JWT Key is missing in appsettings.json");

        if (Encoding.UTF8.GetBytes(keyString).Length < 32)
            throw new Exception("JWT Key must be at least 32 bytes long for HS256");

        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),

        };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
