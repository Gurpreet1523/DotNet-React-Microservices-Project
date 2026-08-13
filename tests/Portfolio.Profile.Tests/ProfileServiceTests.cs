using FluentAssertions;
using Moq;
using Portfolio.Profile.API.Entities;
using Portfolio.Profile.API.Interfaces;
using Portfolio.Profile.API.Services;
using Portfolio.Shared.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Profile.Tests
{
    public class ProfileServiceTests
    {
        private readonly Mock<IProfileRepository> _repoMock = new();
        private readonly ProfileService _service;

        public ProfileServiceTests()
        {
            _service = new ProfileService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnProfileResponse_WhenValidRequest()
        {
            // Arrange
            var request = new CreateProfileRequest
            {
                FullName = "Gurpreet Kaur",
                Title = "Developer",
                Bio = "Dotnet Dev",
                Email = "gpk@test.com",
                Phone = "123456",
                LinkedInUrl = "linkedin.com/john",
                GitHubUrl = "github.com/john",
                ResumeUrl = "resume.com/john"
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Profiles>()));
                //.ReturnsAsync((Profiles p) => p);

            _repoMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be("Gurpreet Kaur");
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProfiles()
        {
            // Arrange
            var profiles = new List<Profiles>
    {
        new() { Id = Guid.NewGuid(), FullName = "A" },
        new() { Id = Guid.NewGuid(), FullName = "B" }
    };

            _repoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(profiles);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoProfiles()
        {
            _repoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Profiles>());

            var result = await _service.GetAllAsync();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProfile_WhenExists()
        {
            var id = Guid.NewGuid();

            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(new Profiles
                {
                    Id = id,
                    FullName = "Gurpreet Kaur"
                });

            var result = await _service.GetByIdAsync(id);

            result.Should().NotBeNull();
            result!.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            var id = Guid.NewGuid();

            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Profiles?)null);

            var result = await _service.GetByIdAsync(id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedProfile_WhenExists()
        {
            var id = Guid.NewGuid();

            var existing = new Profiles
            {
                Id = id,
                FullName = "Old Name"
            };

            var request = new UpdateProfileRequest
            {
                FullName = "New Name",
                Title = "Dev",
                Bio = "Updated bio",
                Phone = "999",
                LinkedInUrl = "linkedin",
                GitHubUrl = "github",
                ResumeUrl = "resume"
            };

            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(existing);

            _repoMock.Setup(r => r.UpdateAsync(existing))
                .Returns(Task.CompletedTask);

            _repoMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // IMPORTANT: GetByIdAsync used again inside service
            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(existing);

            var result = await _service.UpdateAsync(id, request);

            result.Should().NotBeNull();
            result!.FullName.Should().Be("New Name");
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenProfileNotFound()
        {
            var id = Guid.NewGuid();

            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Profiles?)null);

            var request = new UpdateProfileRequest
            {
                FullName = "Test"
            };

            var result = await _service.UpdateAsync(id, request);

            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenProfileExists()
        {
            var id = Guid.NewGuid();

            var profile = new Profiles { Id = id };

            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(profile);

            _repoMock.Setup(r => r.DeleteAsync(profile))
                .Returns(Task.CompletedTask);

            _repoMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var result = await _service.DeleteAsync(id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenProfileNotFound()
        {
            var id = Guid.NewGuid();

            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Profiles?)null);

            var result = await _service.DeleteAsync(id);

            result.Should().BeFalse();
        }
    }
}
