using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechTask.AA.Core.Common;
using TechTask.AA.Core.Models;
using TechTask.AA.Infrastructure.Adapters.Repositories;

namespace TechTask.AA.Infrastructure.Tests.Adapters.Repositories
{
    [TestFixture]
    public class FlightRepositoryTests : RepositoryTestsBase
    {
        [OneTimeSetUp]
        public void Init()
        {
            CreateFlights();
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        public async Task GetFlights_ShouldReturnFlightsCorrectly(TestCaseData data)
        {
            //Arrange
            var sut = new FlightRepository(Db, Mapper);

            //Act
            var flights = await sut.GetFlightsByOriginAndDestinationAsync(data.Origin, data.Destination, CancellationToken.None);

            //Assert
            flights.Should().BeEquivalentTo(data.Expected, opts => opts.Excluding(x => x.Id));
        }

        public class TestCaseData
        {
            public string? Origin { get; set; }
            public string? Destination { get; set; }
            public Flight[] Expected { get; set; } = null!;
        }

        private static IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData
                {
                    Origin = null,
                    Destination = null,
                    Expected = new Flight[]
                    {
                        new()
                    {
                        Origin = "UKK",
                        Destination = "ALA",
                        Departure = new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 14, minute: 05, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                        Arrival = new DateTimeOffset(
                            year: 2023, month: 04, day: 01,
                            hour: 15, minute: 50, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                        Status = FlightStatus.InTime
                    },
                    new()
                    {
                        Origin = "ALA",
                        Destination = "TBS",
                        Departure = new DateTimeOffset(
                            year: 2023, month: 04, day: 02,
                            hour: 18, minute: 30, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                        Arrival = new DateTimeOffset(
                            year: 2023, month: 04, day: 02,
                            hour: 20, minute: 40, second: 0,
                            offset: new TimeSpan(hours: 4, minutes: 0, seconds: 0)),
                        Status = FlightStatus.InTime
                    },
                    new()
                    {
                        Origin = "ALA",
                        Destination = "UKK",
                        Departure = new DateTimeOffset(
                            year: 2023, month: 04, day: 11,
                            hour: 11, minute: 35, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                        Arrival = new DateTimeOffset(
                            year: 2023, month: 04, day: 11,
                            hour: 13, minute: 15, second: 0,
                            offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                        Status = FlightStatus.InTime
                    }
                        }
                };
                yield return new TestCaseData
                {
                    Origin = "ALA",
                    Destination = null,
                    Expected = new Flight[]
                    {
                        new()
                        {
                            Origin = "ALA",
                            Destination = "TBS",
                            Departure = new DateTimeOffset(
                                year: 2023, month: 04, day: 02,
                                hour: 18, minute: 30, second: 0,
                                offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                            Arrival = new DateTimeOffset(
                                year: 2023, month: 04, day: 02,
                                hour: 20, minute: 40, second: 0,
                                offset: new TimeSpan(hours: 4, minutes: 0, seconds: 0)),
                            Status = FlightStatus.InTime
                        },
                        new()
                        {
                            Origin = "ALA",
                            Destination = "UKK",
                            Departure = new DateTimeOffset(
                                year: 2023, month: 04, day: 11,
                                hour: 11, minute: 35, second: 0,
                                offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                            Arrival = new DateTimeOffset(
                                year: 2023, month: 04, day: 11,
                                hour: 13, minute: 15, second: 0,
                                offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                            Status = FlightStatus.InTime
                        }
                    }
                };
                yield return new TestCaseData
                {
                    Origin = null,
                    Destination = "TBS",
                    Expected = new Flight[]
                    {
                        new()
                        {
                            Origin = "ALA",
                            Destination = "TBS",
                            Departure = new DateTimeOffset(
                                year: 2023, month: 04, day: 02,
                                hour: 18, minute: 30, second: 0,
                                offset: new TimeSpan(hours: 6, minutes: 0, seconds: 0)),
                            Arrival = new DateTimeOffset(
                                year: 2023, month: 04, day: 02,
                                hour: 20, minute: 40, second: 0,
                                offset: new TimeSpan(hours: 4, minutes: 0, seconds: 0)),
                            Status = FlightStatus.InTime
                        }
                    }
                };
                yield return new TestCaseData
                {
                    Origin = "TBS",
                    Destination = "ALA",
                    Expected = Array.Empty<Flight>()
                };
            }
        }
    }
}
