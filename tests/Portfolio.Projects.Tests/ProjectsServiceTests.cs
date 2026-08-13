using Moq;
using Portfolio.Projects.API.Interfaces;
using Portfolio.Projects.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Projects.API.Entities;
using FluentAssertions;
using Portfolio.Shared.Contracts.Requests;

namespace Portfolio.Projects.Tests
{
    public class ProjectsServiceTests
    {
        private readonly Mock<IProjectRepository> _repoMock = new();
        private readonly ProjectService _sut;

        public ProjectsServiceTests() => _sut = new ProjectService(_repoMock.Object);

        [Fact]
        public async Task GetAllAsync_ReturnsFeaturedProjects()
        {
            var projects = new List<Project>
        {
            new() { Description = "Used Dot Net Core API with Microservices, JWT and entity framework", Title = "Portfolio App" },
            new() { Description = "Used Dot Net Core API with Microservices, JWT and entity framework", Title = "Finance API" }
        };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(projects);

            var result = await _sut.GetAllAsync();

            result.Should().HaveCount(2);
            //result.Should().AllSatisfy(p => p.IsFeatured.Should().BeTrue());
        }

        [Fact]
        public async Task AddAsync_ShouldReturnCreatedProject()
        {
            var repoMock = new Mock<IProjectRepository>();
            var service = new ProjectService(repoMock.Object);

            var request = new CreateProjectRequest
            {
                Title = "Test Project",
                Description = "testing project required",
                Technologies = "C#, .NET",
                GitHubUrl = "https://github.com/test",
                LiveUrl = "https://test.com",
                ImageUrl = "https://img.com/test.png",
                Featured = true
            };

            // mock repository
            repoMock.Setup(r => r.AddAsync(It.IsAny<Project>()));

            repoMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await service.CreateAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Test Project");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProject_WhenExists()
        {
            var repoMock = new Mock<IProjectRepository>();
            var service = new ProjectService(repoMock.Object);
            var projectId = Guid.NewGuid();

            var project = new Project { Id = projectId, Title = "Test" };

            repoMock.Setup(r => r.GetByIdAsync(projectId))
                .ReturnsAsync(project);

            var result = await service.GetByIdAsync(projectId);

            result.Should().NotBeNull();
            result!.Id.Should().Be(projectId);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            var repoMock = new Mock<IProjectRepository>();
            var service = new ProjectService(repoMock.Object);
            var projectId = Guid.NewGuid();

            repoMock.Setup(r => r.GetByIdAsync(projectId))
                .ReturnsAsync((Project?)null);

            var result = await service.GetByIdAsync(projectId);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedProject_WhenProjectExists()
        {
            // Arrange
            var repoMock = new Mock<IProjectRepository>();
            var service = new ProjectService(repoMock.Object);

            var id = Guid.NewGuid();

            var existingProject = new Project
            {
                Id = id,
                Title = "Old Title"
            };

            var request = new UpdateProjectRequest
            {
                Title = "New Title",
                Description = "Updated Desc",
                Technologies = "C#, .NET",
                GitHubUrl = "https://github.com/test",
                LiveUrl = "https://test.com",
                ImageUrl = "https://img.com/test.png",
                Featured = true
            };

            repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(existingProject);

            repoMock.Setup(r => r.UpdateAsync(existingProject))
                .Returns(Task.CompletedTask);

            repoMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await service.UpdateAsync(id, request);

            // Assert
            result.Should().NotBeNull();
            result!.Title.Should().Be("New Title");
            result!.Description.Should().Be("Updated Desc");

            repoMock.Verify(r => r.UpdateAsync(existingProject), Times.Once);
            repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenProjectNotFound()
        {
            // Arrange
            var repoMock = new Mock<IProjectRepository>();
            var service = new ProjectService(repoMock.Object);

            var id = Guid.NewGuid();

            var request = new UpdateProjectRequest
            {
                Title = "New Title"
            };

            repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Project?)null);

            // Act
            var result = await service.UpdateAsync(id, request);

            // Assert
            result.Should().BeNull();

            repoMock.Verify(r => r.UpdateAsync(It.IsAny<Project>()), Times.Never);
            repoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenRepositoryFails()
        {
            var repoMock = new Mock<IProjectRepository>();
            var service = new ProjectService(repoMock.Object);

            var id = Guid.NewGuid();

            var project = new Project { Id = id };

            var request = new UpdateProjectRequest
            {
                Title = "Test"
            };

            repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(project);

            repoMock.Setup(r => r.UpdateAsync(project))
                .ThrowsAsync(new Exception("DB error"));

            Func<Task> act = async () => await service.UpdateAsync(id, request);

            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenProjectExists()
        {
            // Arrange
            var repoMock = new Mock<IProjectRepository>();
            var service = new ProjectService(repoMock.Object);

            var id = Guid.NewGuid();

            var project = new Project
            {
                Id = id,
                Title = "Test Project"
            };

            repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(project);

            repoMock.Setup(r => r.DeleteAsync(project))
                .Returns(Task.CompletedTask);

            repoMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await service.DeleteAsync(id);

            // Assert
            result.Should().BeTrue();

            repoMock.Verify(r => r.DeleteAsync(project), Times.Once);
            repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNotFound()
        {
            var repoMock = new Mock<IProjectRepository>();
            var service = new ProjectService(repoMock.Object);
            var projectId = Guid.NewGuid();
            var id = Guid.NewGuid();

            repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Project?)null);

            // Act
            var result = await service.DeleteAsync(id);

            // Assert
            result.Should().BeFalse();

            repoMock.Verify(r => r.DeleteAsync(It.IsAny<Project>()), Times.Never);
            repoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var repoMock = new Mock<IProjectRepository>();
            var service = new ProjectService(repoMock.Object);

            var id = Guid.NewGuid();

            var project = new Project { Id = id };

            repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(project);

            repoMock.Setup(r => r.DeleteAsync(project))
                .ThrowsAsync(new Exception("DB Error"));

            // Act
            Func<Task> act = async () => await service.DeleteAsync(id);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
