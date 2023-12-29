using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using TechTask.AA.API.Controllers;
using TechTask.AA.API.DTO;
using TechTask.AA.Core.Common;
using TechTask.AA.Application.Commands;
using TechTask.AA.Application.Queries;

namespace TechTask.AA.API.Tests.Controllers
{
    public class FlightsControllerTests : ControllerTestsBase<FlightsController>
    {
        [Test]
        public async Task GetFlights_ShouldSendCommand()
        {
            ///Arrange
            var getFlightsDto = new GetFlightsDto("origin", "destination");
            var query = new GetFlightsQuery("origin", "destination");
            var queryResult = new GetFlightsQuery.Result(
                1, "origin", "destination",
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 11, minute: 35, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 13, minute: 15, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                FlightStatus.InTime);
            var queryResults = new GetFlightsQuery.Result[] { queryResult };
            var flightDto = new FlightDto(
                1, "origin", "destination",
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 11, minute: 35, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 13, minute: 15, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                FlightStatus.InTime);
            var flightDtos = new FlightDto[] { flightDto };

            _mockMediator.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).Returns(Task.FromResult(queryResults));
            _mockMapper.Setup(m => m.Map<GetFlightsQuery>(getFlightsDto)).Returns(query);
            _mockMapper.Setup(m => m.Map<FlightDto>(queryResult)).Returns(flightDto);

            //Act
            var result = await _controller.GetFlights(getFlightsDto, new CancellationToken());

            //Assert
            _mockMediator.Verify(mock => mock.Send(query, It.IsAny<CancellationToken>()), Times.Once());
            result.Should().BeEquivalentTo(flightDtos);
        }

        [Test]
        public async Task CreateFlight_ShouldSendCommand()
        {
            ///Arrange
            var createFlightDto = new CreateFlightDto(
                "origin", "destination",
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 11, minute: 35, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 13, minute: 15, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)));
            var command = new CreateFlightCommand(
                "origin", "destination",
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 11, minute: 35, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 13, minute: 15, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)));
            var commandResult = new CreateFlightCommand.Result(
                1, "origin", "destination",
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 11, minute: 35, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 13, minute: 15, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                FlightStatus.InTime);
            var flightDto = new FlightDto(
                1, "origin", "destination",
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 11, minute: 35, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 13, minute: 15, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                FlightStatus.InTime);

            _mockMediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Returns(Task.FromResult(commandResult));
            _mockMapper.Setup(m => m.Map<CreateFlightCommand>(createFlightDto)).Returns(command);
            _mockMapper.Setup(m => m.Map<FlightDto>(commandResult)).Returns(flightDto);

            //Act
            var result = await _controller.CreateFlight(createFlightDto, new CancellationToken());

            //Assert
            _mockMediator.Verify(mock => mock.Send(command, It.IsAny<CancellationToken>()), Times.Once());
            result.Should().BeEquivalentTo(flightDto);
        }

        [Test]
        public async Task UpdateFlightStatus_ShouldSendCommand()
        {
            ///Arrange
            var updateFlightDto = new UpdateFlightStatusDto(1, FlightStatus.Delayed);
            var command = new UpdateFlightStatusCommand(1, FlightStatus.Delayed);
            var commandResult = new UpdateFlightStatusCommand.Result(
                1, "origin", "destination",
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 11, minute: 35, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 13, minute: 15, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                FlightStatus.Delayed);
            var flightDto = new FlightDto(
                1, "origin", "destination",
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 11, minute: 35, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                new DateTimeOffset(
                    year: 2023, month: 04, day: 11,
                    hour: 13, minute: 15, second: 0,
                    offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                FlightStatus.Delayed);

            _mockMediator.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).Returns(Task.FromResult(commandResult));
            _mockMapper.Setup(m => m.Map<UpdateFlightStatusCommand>(updateFlightDto)).Returns(command);
            _mockMapper.Setup(m => m.Map<FlightDto>(commandResult)).Returns(flightDto);

            //Act
            var result = await _controller.UpdateFlightStatus(updateFlightDto, new CancellationToken());

            //Assert
            _mockMediator.Verify(mock => mock.Send(command, It.IsAny<CancellationToken>()), Times.Once());
            result.Should().BeEquivalentTo(flightDto);
        }
    }
}
