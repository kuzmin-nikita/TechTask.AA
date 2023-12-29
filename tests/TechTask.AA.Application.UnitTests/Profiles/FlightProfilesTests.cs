using AutoMapper;
using NUnit.Framework;
using System;
using TechTask.AA.Core.Common;
using TechTask.AA.Core.Models;
using TechTask.AA.Application.Commands;
using TechTask.AA.Application.Profiles;
using TechTask.AA.Application.Queries;

namespace TechTask.Application.Tests.Profiles
{
    public class FlightProfilesTests
    {
        private IMapper _mapper;
        public Flight Flight;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FlightProfiles>();
            });
            _mapper = config.CreateMapper();

            Flight = new Flight
            {
                Id = 1,
                Origin = "TestOrigin",
                Destination = "TestDestination",
                Departure = new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Arrival = new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                Status = FlightStatus.InTime
            };
        }

        [Test]
        public void CreateMap_Flight_CreateFlightCommandResult()
        {
            //Arrange
            var expectedResult = new CreateFlightCommand.Result(
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
            var actual = _mapper.Map<CreateFlightCommand.Result>(Flight);

            //Assert
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void CreateMap_Flight_UpdateFlightStatusCommandResult()
        {
            //Arrange
            var expectedResult = new UpdateFlightStatusCommand.Result(
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
            var actual = _mapper.Map<UpdateFlightStatusCommand.Result>(Flight);

            //Assert
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        public void CreateMap_Flight_GetFlightsQueryResult()
        {
            //Arrange
            var expectedResult = new GetFlightsQuery.Result(
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
            var actual = _mapper.Map<GetFlightsQuery.Result>(Flight);

            //Assert
            Assert.AreEqual(expectedResult, actual);
        }
    }
}
