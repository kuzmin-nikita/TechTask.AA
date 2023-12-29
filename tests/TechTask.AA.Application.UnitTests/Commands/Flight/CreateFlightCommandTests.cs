using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTask.AA.Application.Commands;

namespace TechTask.Application.Tests.Queries.Flight
{
    [TestFixture]
    public class CreateFlightCommandTests
    {
        [Test]
        public void HandleCommand_ShouldReturnErrorMessages_WhenValidationFailed()
        {
            //Arrange
            var origin = (string)null;
            var destination = new string('*', 257);
            var departure = new DateTimeOffset();
            var arrival = new DateTimeOffset();
            var command = new CreateFlightCommand(origin, destination, departure, arrival);
            var sut = new CreateFlightCommand.Validator();
            var expectedErrorMessages = new List<string>
            {
                "'Origin' must not be empty.",
                "'Destination' must be between 1 and 256 characters. You entered 257 characters.",
                "'Departure' must not be empty.",
                "'Arrival' must not be empty."
            };

            //Act
            var actual = sut.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();

            //Assert
            actual.Should().BeEquivalentTo(expectedErrorMessages);
        }
    }
}
