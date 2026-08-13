using FluentAssertions;
using Moq;
using Portfolio.Auth.API.Models;
using Portfolio.Auth.API.Services;
using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Auth.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IJwtTokenService> _jwtMock;

        private readonly AuthDbContext _context;

        private readonly AuthService _sut;

        public AuthServiceTests()
        {
            _jwtMock = new Mock<IJwtTokenService>();

            var options =
                new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AuthDbContext(options);

            _sut = new AuthService(
                _context,
                _jwtMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsTokens()
        {
            // Arrange

            var user = new User
            {
                Id = Guid.NewGuid(),

                Email = "admin@test.com",//, "admin@gurpreet.dev"

                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(
                        "Admin!123"),

                Role = "Admin",

                CreatedAt = DateTime.UtcNow,

                IsActive = true
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            //var userDto = new UserDto
            //{
            //    Id = user.Id,
            //    Email = user.Email,
            //    Role = user.Role,
            //};

            _jwtMock.Setup(x =>
                x.GenerateAccessToken(It.IsAny<UserDto>()))
                .Returns("accessToken");

            _jwtMock.Setup(x =>
                x.GenerateRefreshToken())
                .Returns("refreshToken");

            // Act

            var result =
                await _sut.LoginAsync(
                    new LoginRequest
                    {
                        Email = "admin@test.com",//"admin@gurpreet.dev",
                        Password = "Admin!123" //"Password@123"
                    });

            // Assert

            result.Success.Should().BeTrue();

            result.AccessToken.Should()
                .Be("accessToken");

            result.RefreshToken.Should()
                .Be("refreshToken");
        }

        [Fact]
        public async Task LoginAsync_InvalidPassword_ReturnsFailure()
        {
            // Arrange

            var user = new User
            {
                Id = Guid.NewGuid(),

                Email = "admin@gurpreet.dev",

                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(
                        "correct-password"),

                Role = "Admin"
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            // Act

            var result =
                await _sut.LoginAsync(
                    new LoginRequest
                    {
                        Email = "admin@gurpreet.dev",
                        Password = "wrong-password"
                    });

            // Assert

            result.Success.Should().BeFalse();

            result.Message.Should()
                .Contain("Invalid");
        }

        [Fact]
        public async Task LoginAsync_UserNotFound_ReturnsFailure()
        {
            // Act

            var result =
                await _sut.LoginAsync(
                    new LoginRequest
                    {
                        Email = "admin@test.com",
                        Password = "Admin!123"
                    });

            // Assert

            result.Success.Should().BeFalse();

            result.Message.Should()
                .Contain("User not found");
        }
    }
}
