using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TechTask.AA.Application.Queries;

namespace TechTask.Application.Tests.Queries.Flight
{
    [TestFixture]
    public class GetFlightsQueryTests
    {
        [Test]
        public void HandleQuery_ShouldReturnErrorMessages_WhenValidationFailed()
        {
            //Arrange
            var origin = new string('*', 257);
            var destination = new string('*', 257);
            var query = new GetFlightsQuery(origin, destination);
            var sut = new GetFlightsQuery.Validator();
            var expectedErrorMessages = new List<string>
            {
                "The length of 'Origin' must be 256 characters or fewer. You entered 257 characters.",
                "The length of 'Destination' must be 256 characters or fewer. You entered 257 characters."
            };

            //Act
            var actual = sut.Validate(query).Errors.Select(e => e.ErrorMessage).ToList();

            //Assert
            actual.Should().BeEquivalentTo(expectedErrorMessages);
        }
    }
}
