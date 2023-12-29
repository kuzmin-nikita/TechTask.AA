using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using TechTask.AA.API.Controllers;
using TechTask.AA.API.DTO;
using TechTask.AA.Application.Commands;

namespace TechTask.AA.API.Tests.Controllers
{
    public class IdentityControllerTests : ControllerTestsBase<IdentityController>
    {
        [Test]
        public async Task Authorize_ShouldSendCommand()
        {
            ///Arrange
            var authorizeDto = new AuthorizeDto("username", "password");
            var command = new AuthorizeCommand("username", "password");
            var commandResult = new AuthorizeCommand.Result("token");
            var identityDto = new IdentityDto("token");
            _mockMediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Returns(Task.FromResult(commandResult));
            _mockMapper.Setup(m => m.Map<AuthorizeCommand>(authorizeDto)).Returns(command);
            _mockMapper.Setup(m => m.Map<IdentityDto>(commandResult)).Returns(identityDto);

            //Act
            var result = await _controller.Authorize(authorizeDto, new CancellationToken());

            //Assert
            _mockMediator.Verify(mock => mock.Send(command, It.IsAny<CancellationToken>()), Times.Once());
            Assert.AreEqual(identityDto, result);
        }
    }
}
