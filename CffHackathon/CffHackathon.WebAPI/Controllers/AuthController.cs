using Microsoft.AspNetCore.Mvc;
using CffHackathon.Application.Services;
using CffHackathon.Application.DTOs.Auth;
using CffHackathon.Application.Common.Models.Response;
using Microsoft.AspNetCore.Authorization;

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

    [HttpPost("AssignRole")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> AssignRole([FromForm] string userId,string roleName)
    {
        var result= await _authService.AssignedRole(userId,roleName);
        var response=Response<string>.Success(result,200);
        return Ok(response);
    }

    [HttpGet("Waiters")]
    public async Task<IActionResult> GetWaiters()
    {
        var waiters = await _authService.GetUsersByRoleAsync("Waiter");
        var response = Response<List<UserDto>>.Success(waiters, 200);
        return Ok(response);
    }
}
