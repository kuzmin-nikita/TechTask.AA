using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTask.AA.Core.Exceptions;
using TechTask.AA.Core.Helpers;
using TechTask.AA.Core.Models;
using TechTask.AA.Core.Ports.Repositories;
using TechTask.AA.Application.Commands;

namespace TechTask.Application.Tests.Queries.Flight
{
    [TestFixture]
    public class AuthorizeCommandTests
    {
        [Test]
        public void HandleCommand_ShouldReturnErrorMessages_WhenValidationFailed()
        {
            var username = (string)null;
            var password = new string('*', 257);
            var command = new AuthorizeCommand(username, password);
            var sut = new AuthorizeCommand.Validator();
            var expectedErrorMessages = new List<string>
            {
                "'Username' must not be empty.",
                "'Password' must be between 1 and 256 characters. You entered 257 characters."
            };

            //Act
            var actual = sut.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();

            //Assert
            actual.Should().BeEquivalentTo(expectedErrorMessages);
        }

        [Test]
        public void HandleCommand_ShouldThrowException_WhenUserNotFound()
        {
            //Arrange
            var username = "username";
            var password = "password";

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByUsernameAsync(username, It.IsAny<CancellationToken>())).Returns(Task.FromResult((User)null));

            var mockRoleRepository = new Mock<IRoleRepository>();

            var command = new AuthorizeCommand(username, password);
            var sut = new AuthorizeCommand.Handler(mockUserRepository.Object, mockRoleRepository.Object);
            var expectedExceptionMessage = $"User with Username: '{username}' was not found";

            //Act
            var exception = Assert.ThrowsAsync<NotFoundException>(async Task () => { await sut.Handle(command, CancellationToken.None); });

            //Assert
            exception.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }

        [Test]
        public void HandleCommand_ShouldThrowException_WhenPasswordNotCorrect()
        {
            //Arrange
            var username = "username";
            var password = "password";
            var wrongPassword = "wrongPassword";

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByUsernameAsync(username, It.IsAny<CancellationToken>())).Returns(Task.FromResult(new User { Id = 1, Username = username, Password = HashHelper.ComputeHash(password), RoleId = 1 }));

            var mockRoleRepository = new Mock<IRoleRepository>();

            var command = new AuthorizeCommand(username, wrongPassword);
            var sut = new AuthorizeCommand.Handler(mockUserRepository.Object, mockRoleRepository.Object);
            var expectedExceptionMessage = $"Incorrect password for user with Username: '{username}'";

            //Act
            var exception = Assert.ThrowsAsync<BadRequestException>(async Task () => { await sut.Handle(command, CancellationToken.None); });

            //Assert
            exception.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }

        [Test]
        public void HandleCommand_ShouldThrowException_WhenRoleNotFound()
        {
            //Arrange
            var username = "username";
            var password = "password";
            var roleId = 10;

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByUsernameAsync(username, It.IsAny<CancellationToken>())).Returns(Task.FromResult(new User { Id = 1, Username = username, Password = HashHelper.ComputeHash(password), RoleId = roleId }));

            var mockRoleRepository = new Mock<IRoleRepository>();
            mockRoleRepository.Setup(x => x.GetRoleByIdAsync(roleId, It.IsAny<CancellationToken>())).Returns(Task.FromResult((Role)null));

            var command = new AuthorizeCommand(username, password);
            var sut = new AuthorizeCommand.Handler(mockUserRepository.Object, mockRoleRepository.Object);
            var expectedExceptionMessage = $"Role with Id: '{roleId}' was not found";

            //Act
            var exception = Assert.ThrowsAsync<NotFoundException>(async Task () => { await sut.Handle(command, CancellationToken.None); });

            //Assert
            exception.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }
    }
}
