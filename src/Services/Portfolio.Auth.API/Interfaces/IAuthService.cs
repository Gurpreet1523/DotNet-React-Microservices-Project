using Microsoft.AspNetCore.Identity.Data;
using Portfolio.Auth.API.Models;
using LoginRequest = Portfolio.Auth.API.Models.LoginRequest;
using RegisterRequest = Portfolio.Auth.API.Models.RegisterRequest;

namespace Portfolio.Auth.API.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);

        Task<AuthResponse> RefreshTokenAsync(string refreshToken);

        Task<bool> RevokeTokensAsync(Guid userId);
        Task<string> RegisterAsync(RegisterRequest request);
    }
}
