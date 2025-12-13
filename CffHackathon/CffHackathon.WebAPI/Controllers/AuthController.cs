using Microsoft.AspNetCore.Mvc;
using CffHackathon.Application.Services;
using CffHackathon.Application.DTOs.Auth;
using CffHackathon.Application.Common.Models.Response;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromForm] RegisterDto dto)
    {
            await _authService.RegisterAsync(dto.Email, dto.Password);
        var response = Response<string>.Success("User registered successfully", 201);
            return Ok(response);
        
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromForm] LoginDto dto)
    {
        
            var token = await _authService.LoginAsync(dto.Email, dto.Password);
        var response=Response<string>.Success(token, 200);
        return Ok(response);
    }
}
