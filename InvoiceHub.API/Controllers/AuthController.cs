using InvoiceHub.Application.Interfaces;
using InvoiceHub.Domain.Users;
using InvoiceHub.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;
    private readonly PasswordHasher<User> _passwordHasher;
    
    public AuthController(IUserRepository userRepository, 
        JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordHasher = new PasswordHasher<User>();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            return BadRequest("User already exists.");

        var user = new User(request.Email, "");
        var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
        var newUser = new User(request.Email, hashedPassword);

        await _userRepository.AddAsync(newUser);
        
        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            return Unauthorized("Invalid credentials.");

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);
        
        if(result == PasswordVerificationResult.Failed)
            return Unauthorized("Invalid credentials.");

        var token = _jwtService.GenerateToken(user);
        
        return Ok(new { Token = token });
    }
}

public class RegisterRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class AuthResponse
{
    public string Token { get; set; } = default!;
}