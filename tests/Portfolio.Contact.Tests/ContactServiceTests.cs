using FluentAssertions;
using Moq;
using Portfolio.Contact.API.Entities;
using Portfolio.Contact.API.Interfaces;
using Portfolio.Contact.API.Services;
using Portfolio.Shared.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Contact.Tests
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _repoMock = new();
        private readonly ContactService _service;

        public ContactServiceTests()
        {
            _service = new ContactService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnContactMessageDto_WhenValidRequest()
        {
            // Arrange
            var request = new CreateContactMessageRequest
            {
                Name = "Gurpreet Kaur",
                Email = "gpk@test.com",
                Subject = "Support",
                Message = "Need help"
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<ContactMessage>()));
                //.ReturnsAsync((ContactMessage c) => c);

            _repoMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Gurpreet Kaur");
            result.Email.Should().Be("gpk@test.com");
            result.Subject.Should().Be("Support");
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var request = new CreateContactMessageRequest
            {
                Name = "Gurpreet Kaur",
                Email = "gpk@test.com",
                Subject = "Support",
                Message = "Help"
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<ContactMessage>()))
                .ThrowsAsync(new Exception("DB error"));

            // Act
            Func<Task> act = async () => await _service.CreateAsync(request);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMessages()
        {
            // Arrange
            var messages = new List<ContactMessage>
                   {
                       new() { Id = Guid.NewGuid(), Name = "A", Email = "a@test.com" },
                        new() { Id = Guid.NewGuid(), Name = "B", Email = "b@test.com" }
                    };

            _repoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(messages);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoMessages()
        {
            _repoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<ContactMessage>());

            var result = await _service.GetAllAsync();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnMessage_WhenExists()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(new ContactMessage
                {
                    Id = id,
                    Name = "Gurpreet Kaur",
                    Email = "gpk@test.com",
                    Subject = "Help",
                    Message = "Need support"
                });

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(id);
            result.Name.Should().Be("Gurpreet Kaur");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            var id = Guid.NewGuid();

            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((ContactMessage?)null);

            var result = await _service.GetByIdAsync(id);

            result.Should().BeNull();
        }
    }
}
