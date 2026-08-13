using Portfolio.Auth.API.Interfaces;
using Portfolio.Auth.API.Models;
using Portfolio.Shared.Infrastructure.Interfaces;
using Portfolio.Auth.API.Data; // your DbContext
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Portfolio.Shared.Contracts.DTO;

namespace Portfolio.Auth.API.Services;

public class AuthService : IAuthService
{
    private readonly AuthDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        AuthDbContext context,
        IJwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    // LOGIN
    public async Task<AuthResponse> LoginAsync(
     LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Email == request.Email);

        // CHECK NULL FIRST
        if (user == null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        var isValidPassword =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

        if (!isValidPassword)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Invalid password"
            };
        }

        // CREATE DTO AFTER NULL CHECK
        var userDto = new UserDto
        {
            Id = user.Id,

            Role = user.Role,

            Email = user.Email
        };

        var accessToken =
            _jwtTokenService
            .GenerateAccessToken(userDto);

        var refreshToken =
            _jwtTokenService
            .GenerateRefreshToken();

        // SAVE REFRESH TOKEN
        _context.RefreshTokens.Add(
            new RefreshToken
            {
                UserId = user.Id,

                Token = refreshToken,

                ExpiresAt =
                    DateTime.UtcNow.AddDays(7)
            });

        await _context.SaveChangesAsync();

        return new AuthResponse
        {
            Success = true,

            Message = "Login successful",

            AccessToken = accessToken,

            RefreshToken = refreshToken
        };
    }

    // REFRESH TOKEN
    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (token == null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Invalid refresh token"
            };
        }
        var user = new UserDto
        {
            Id = token.User.Id,
            Email = token.User.Email,
            Role = token.User.Role
        };

        var newAccessToken = _jwtTokenService.GenerateAccessToken(user);

        return new AuthResponse
        {
            Success = true,
            AccessToken = newAccessToken,
            RefreshToken = refreshToken
        };
    }

    // LOGOUT
    public async Task<bool> RevokeTokensAsync(Guid userId)
    {
        var tokens = await _context.RefreshTokens
            .Where(x => x.UserId == userId)
            .ToListAsync();

        foreach (var token in tokens)
        {
            token.IsRevoked = true;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    //RegisterUser

    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        var existingUser =
            await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Email == request.Email);

        if (existingUser != null)
        {
            return "User already exists";
        }

        var user = new User
        {
            Id = Guid.NewGuid(),

            Email = request.Email,

            PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    request.Password),

            Role = "Admin",

            CreatedAt = DateTime.UtcNow,

            IsActive = true
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return "User Added Successfully";
    }
}
