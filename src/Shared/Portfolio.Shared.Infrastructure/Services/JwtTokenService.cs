using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Infrastructure.Interfaces;
using Portfolio.Shared.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Shared.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _settings;

        public JwtTokenService(IOptions<JwtSettings> settings)
            => _settings = settings.Value;

        public string GenerateAccessToken(UserDto user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.SecretKey));

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpiryMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken() =>
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
