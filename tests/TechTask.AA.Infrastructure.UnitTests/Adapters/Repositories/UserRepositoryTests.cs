using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechTask.AA.Core.Helpers;
using TechTask.AA.Core.Models;
using TechTask.AA.Infrastructure.Adapters.Repositories;

namespace TechTask.AA.Infrastructure.Tests.Adapters.Repositories
{
    [TestFixture]
    public class UserRepositoryTests : RepositoryTestsBase
    {
        [OneTimeSetUp]
        public void Init()
        {
            CreateUsers();
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        public async Task GetUsers_ShouldReturnUserCorrectly(TestCaseData data)
        {
            //Arrange
            var sut = new UserRepository(Db, Mapper);

            //Act
            var Users = await sut.GetUserByUsernameAsync(data.Username, CancellationToken.None);

            //Assert
            Users.Should().BeEquivalentTo(data.Expected, opts => opts.Excluding(x => x.Id));
        }

        public class TestCaseData
        {
            public string? Username { get; set; }
            public User? Expected { get; set; }
        }

        private static IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData
                {
                    Username = null,
                    Expected = null
                };
                yield return new TestCaseData
                {
                    Username = "newUser",
                    Expected = null
                };
                yield return new TestCaseData
                {
                    Username = "user",
                    Expected = new()
                    {
                        Username = "user",
                        Password = HashHelper.ComputeHash("user"),
                        RoleId = 2
                    }
                };
            }
        }
    }
}
