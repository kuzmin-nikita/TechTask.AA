using AutoMapper;
using NUnit.Framework;
using System;
using TechTask.AA.API.DTO;
using TechTask.AA.Core.Common;
using TeshTask.API.Profiles;
using TechTask.AA.Application.Commands;
using TechTask.AA.Application.Queries;

namespace TechTask.AA.API.Tests.Profiles
{
    public class FlightProfilesTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FlightProfiles>();
            });
            _mapper = config.CreateMapper();
        }

        #region GetFlightsQuery Mapping
        [Test]
        public void CreateMap_GetFlightsDto_GetFlightsQuery()
        {
            //Arrange
            var dto = new GetFlightsDto(
                Origin: "TestOrigin",
                Destination: "TestDestination");
            var expectedQuery = new GetFlightsQuery(
                Origin: "TestOrigin",
                Destination: "TestDestination");

            //Act
            var actual = _mapper.Map<GetFlightsQuery>(dto);

            //Assert
            Assert.AreEqual(expectedQuery, actual);
        }

        [Test]
        public void CreateMap_GetFlightsQueryResult_FlightDto()
        {
            //Arrange
            var result = new GetFlightsQuery.Result(
                Id: 1,
                Origin: "TestOrigin",
                Destination: "TestDestination",
                Departure: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Arrival: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Status: FlightStatus.InTime);
            var expectedDto = new FlightDto(
                Id: 1,
                Origin: "TestOrigin",
                Destination: "TestDestination",
                Departure: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Arrival: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Status: FlightStatus.InTime);

            //Act
            var actual = _mapper.Map<FlightDto>(result);

            //Assert
            Assert.AreEqual(expectedDto, actual);
        }
        #endregion

        #region CreateFlightCommand Mapping
        [Test]
        public void CreateMap_CreateFlightDto_CreateFlightCommand()
        {
            //Arrange
            var dto = new CreateFlightDto(
                Origin: "TestOrigin",
                Destination: "TestDestination",
                Departure: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Arrival: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)));
            var expectedCommand = new CreateFlightCommand(
                Origin: "TestOrigin",
                Destination: "TestDestination",
                Departure: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Arrival: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)));

            //Act
            var actual = _mapper.Map<CreateFlightCommand>(dto);

            //Assert
            Assert.AreEqual(expectedCommand, actual);
        }

        [Test]
        public void CreateMap_CreateFlightCommandResult_CreateFlightDto()
        {
            //Arrange
            var result = new CreateFlightCommand.Result(
                Id: 1,
                Origin: "TestOrigin",
                Destination: "TestDestination",
                Departure: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Arrival: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Status: FlightStatus.InTime);
            var expectedDto = new FlightDto(
                Id: 1,
                Origin: "TestOrigin",
                Destination: "TestDestination",
                Departure: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Arrival: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Status: FlightStatus.InTime);

            //Act
            var actual = _mapper.Map<FlightDto>(result);

            //Assert
            Assert.AreEqual(expectedDto, actual);
        }
        #endregion

        #region UpdateFlightStatusCommand Mapping
        [Test]
        public void CreateMap_CUpdateFlightStatusDto_UpdateFlightStatusCommand()
        {
            //Arrange
            var dto = new UpdateFlightStatusDto(
                Id: 10,
                Status: FlightStatus.InTime);
            var expectedCommand = new UpdateFlightStatusCommand(
                Id: 10,
                Status: FlightStatus.InTime);

            //Act
            var actual = _mapper.Map<UpdateFlightStatusCommand>(dto);

            //Assert
            Assert.AreEqual(expectedCommand, actual);
        }

        [Test]
        public void CreateMap_UpdateFlightStatusCommandResult_CreateFlightDto()
        {
            //Arrange
            var result = new UpdateFlightStatusCommand.Result(
                Id: 1,
                Origin: "TestOrigin",
                Destination: "TestDestination",
                Departure: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Arrival: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Status: FlightStatus.InTime);
            var expectedDto = new FlightDto(
                Id: 1,
                Origin: "TestOrigin",
                Destination: "TestDestination",
                Departure: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Arrival: new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Status: FlightStatus.InTime);

            //Act
            var actual = _mapper.Map<FlightDto>(result);

            //Assert
            Assert.AreEqual(expectedDto, actual);
        }
        #endregion
    }
}
