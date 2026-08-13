using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Auth.API.Interfaces;
using Portfolio.Auth.API.Models;
using Portfolio.Shared.Contracts.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Auth.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /*private readonly IJwtTokenService _jwtService;

        public AuthController(IJwtTokenService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            // Fake user (replace with DB later)
            var user = new UserDto
            {
                Id = Guid.NewGuid(),
                Email = "test@test.com",
                Role = "Admin"
            };

            var token = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            return Ok(new
            {
                AccessToken = token,
                RefreshToken = refreshToken
            });
        }*/
       private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.Success) return Unauthorized(result.Message);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            return Ok(new
            {
                userId,
                email
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request.RefreshToken);
            if (!result.Success) return Unauthorized();
            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await _authService.RevokeTokensAsync(Guid.Parse(userId));
            return NoContent();
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result =await _authService.RegisterAsync(request);

            return Ok(result);
        }
    }
}

