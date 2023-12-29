using NUnit.Framework;
using System;
using TechTask.AA.Core.Common;
using TechTask.AA.Core.Exceptions;
using TechTask.AA.Core.Models;

namespace TechTask.AA.Core.Tests.Models
{
    [TestFixture]
    public class FlightTests
    {
        [Test]
        public void CtorFlight_ShouldThrowException_WhenOriginEqualsDestination()
        {
            //Arrange
            var origin = "Test";
            var destination = "Test";
            var departure = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 11, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));
            var arrival = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 11, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));

            var expectedExceptionMessage = "Origin city cannot be same as destination city";

            //Act
            var exception = Assert.Throws<BadRequestException>(() => { new Flight(origin, destination, departure, arrival); });

            //Assert
            Assert.AreEqual(expectedExceptionMessage, $"{exception.Message}");
        }

        [Test]
        public void CtorFlight_ShouldThrowException_WhenDepartureEqualsArrival()
        {
            //Arrange
            var origin = "Origin";
            var destination = "Destination";
            var departure = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 11, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));
            var arrival = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 11, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));

            var expectedExceptionMessage = "Departure time must be less than arrival time";

            //Act
            var exception = Assert.Throws<BadRequestException>(() => { new Flight(origin, destination, departure, arrival); });

            //Assert
            Assert.AreEqual(expectedExceptionMessage, $"{exception.Message}");
        }

        [Test]
        public void CtorFlight_ShouldThrowException_WhenDepartureIsGreaterThanArrival()
        {
            //Arrange
            var origin = "Origin";
            var destination = "Destination";
            var departure = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 12, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));
            var arrival = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 11, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));

            var expectedExceptionMessage = "Departure time must be less than arrival time";

            //Act
            var exception = Assert.Throws<BadRequestException>(() => { new Flight(origin, destination, departure, arrival); });

            //Assert
            Assert.AreEqual(expectedExceptionMessage, $"{exception.Message}");
        }

        [Test]
        public void CtorFlight_ShouldCreateFlight_WhenDataIsCorrect()
        {
            //Arrange
            var id = 0;
            var origin = "Origin";
            var destination = "Destination";
            var departure = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 10, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));
            var arrival = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 11, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));
            var status = FlightStatus.InTime;

            //Act
            var flight = new Flight(origin, destination, departure, arrival);

            //Assert
            Assert.AreEqual(id, flight.Id);
            Assert.AreEqual(origin, flight.Origin);
            Assert.AreEqual(destination, flight.Destination);
            Assert.AreEqual(departure, flight.Departure);
            Assert.AreEqual(arrival, flight.Arrival);
            Assert.AreEqual(status, flight.Status);
        }

        [Test]
        public void UpdateFlightStatus_ShouldThrowException_WhenNewStatusIsLessThanCurrent()
        {
            //Arrange
            var origin = "Origin";
            var destination = "Destination";
            var departure = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 10, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));
            var arrival = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 11, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));

            var flight = new Flight(origin, destination, departure, arrival);
            flight.UpdateStatus(FlightStatus.Cancelled);

            var newStatus = FlightStatus.InTime;

            var expectedExceptionMessage = $"Flight status '{newStatus}' cannot be applied to flight with status '{flight.Status}'";

            //Act
            var exception = Assert.Throws<BadRequestException>(() => { flight.UpdateStatus(newStatus); });

            //Assert
            Assert.AreEqual(expectedExceptionMessage, $"{exception.Message}");
        }

        [Test]
        public void UpdateFlightStatus_ShouldReturnFlight_WhenNewStatusEqualsCurrent()
        {
            //Arrange
            var id = 0;
            var origin = "Origin";
            var destination = "Destination";
            var departure = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 10, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));
            var arrival = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 11, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));

            var flight = new Flight(origin, destination, departure, arrival);

            var newStatus = FlightStatus.InTime;

            //Act
            flight.UpdateStatus(newStatus);

            //Assert
            Assert.AreEqual(id, flight.Id);
            Assert.AreEqual(origin, flight.Origin);
            Assert.AreEqual(destination, flight.Destination);
            Assert.AreEqual(departure, flight.Departure);
            Assert.AreEqual(arrival, flight.Arrival);
            Assert.AreEqual(newStatus, flight.Status);
        }

        [Test]
        public void UpdateFlightStatus_ShouldUpdateFlight_WhenDataIsCorrect()
        {
            //Arrange
            var id = 0;
            var origin = "Origin";
            var destination = "Destination";
            var departure = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 10, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));
            var arrival = new DateTimeOffset(
                year: 1111, month: 11, day: 11,
                hour: 11, minute: 11, second: 11,
                offset: new TimeSpan(hours: 1, minutes: 0, seconds: 0));

            var flight = new Flight(origin, destination, departure, arrival);

            var newStatus = FlightStatus.Delayed;

            //Act
            flight.UpdateStatus(newStatus);

            //Assert
            Assert.AreEqual(id, flight.Id);
            Assert.AreEqual(origin, flight.Origin);
            Assert.AreEqual(destination, flight.Destination);
            Assert.AreEqual(departure, flight.Departure);
            Assert.AreEqual(arrival, flight.Arrival);
            Assert.AreEqual(newStatus, flight.Status);
        }
    }
}
