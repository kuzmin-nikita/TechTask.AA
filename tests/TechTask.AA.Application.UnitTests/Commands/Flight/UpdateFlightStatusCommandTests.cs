using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTask.AA.Application.Commands;
using TechTask.AA.Core.Common;
using TechTask.AA.Core.Exceptions;
using TechTask.AA.Core.Ports.Repositories;

namespace TechTask.Application.Tests.Queries.Flight
{
    [TestFixture]
    public class UpdateFlightStatusCommandTests
    {
        [Test]
        public void HandleCommand_ShouldReturnErrorMessages_WhenValidationFailed()
        {
            //Arrange
            var id = 0;
            var status = FlightStatus.InTime;
            var command = new UpdateFlightStatusCommand(id, status);
            var sut = new UpdateFlightStatusCommand.Validator();
            var expectedErrorMessages = new List<string>
            {
                "'Id' must be between 0 and 2147483647 (exclusive). You entered 0."
            };

            //Act
            var actual = sut.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();

            //Assert
            actual.Should().BeEquivalentTo(expectedErrorMessages);
        }

        [Test]
        public void HandleCommand_ShouldThrowException_WhenFlightNotFound()
        {
            //Arrange
            var id = 1;
            var status = FlightStatus.InTime;

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(x => x.GetFlightByIdAsync(id, It.IsAny<CancellationToken>())).Returns(Task.FromResult((AA.Core.Models.Flight)null));

            var mockMapper = new Mock<IMapper>();

            var command = new UpdateFlightStatusCommand(id, status);
            var sut = new UpdateFlightStatusCommand.Handler(mockRepository.Object, mockMapper.Object);
            var expectedExceptionMessage = $"Flight with Id: {id} was not found";

            //Act
            var exception = Assert.ThrowsAsync<NotFoundException>(async Task () => { await sut.Handle(command, CancellationToken.None); });

            //Assert
            exception.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }
    }
}
