using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TechTask.AA.Core.Common;
using TechTask.AA.Core.Helpers;
using TechTask.AA.DAL;
using TechTask.AA.DAL.DAO;
using TechTask.AA.Infrastructure.Profiles;

namespace TechTask.AA.Infrastructure.Tests.Adapters.Repositories
{
    public class RepositoryTestsBase : IDisposable
    {
        private readonly IServiceCollection _collection = new ServiceCollection();
        private IServiceProvider? _provider;
        private IServiceScope? _scope;
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected IServiceCollection ServiceCollection => _collection;
        protected IServiceProvider Provider => _provider ??= _collection.BuildServiceProvider();
        protected IServiceScope Scope => _scope ??= Provider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        protected TechTaskDbContext Db => Scope.ServiceProvider.GetRequiredService<TechTaskDbContext>();
        protected IMapper Mapper => Scope.ServiceProvider.GetRequiredService<IMapper>();

        public RepositoryTestsBase()
        {
            ServiceCollection.AddDbContext<TechTaskDbContext>(options => options.UseInMemoryDatabase(_dbName));
            ServiceCollection.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<FlightProfile>();
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<UserProfile>();
            });
        }

        public List<FlightDao> CreateFlights()
        {
            var flights = new List<FlightDao>
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
            };
            Db.Flights.AddRange(flights);
            Db.SaveChanges();
            return flights;
        }

        public List<RoleDao> CreateRoles()
        {
            var roles = new List<RoleDao>
            {
                new() { Code = "Moderator" },
                new() { Code = "User" }
            };
            Db.Roles.AddRange(roles);
            Db.SaveChanges();
            return roles;
        }

        public List<UserDao> CreateUsers()
        {
            var users = new List<UserDao>
            {
                new() { Username = "moderator", Password = HashHelper.ComputeHash("moderator"), RoleId = 1 },
                new() { Username = "user", Password = HashHelper.ComputeHash("user"), RoleId = 2 },
            };
            Db.Users.AddRange(users);
            Db.SaveChanges();
            return users;
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}
