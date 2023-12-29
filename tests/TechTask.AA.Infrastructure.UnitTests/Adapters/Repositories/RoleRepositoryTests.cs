using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechTask.AA.Core.Models;
using TechTask.AA.Infrastructure.Adapters.Repositories;

namespace TechTask.AA.Infrastructure.Tests.Adapters.Repositories
{
    [TestFixture]
    public class RoleRepositoryTests : RepositoryTestsBase
    {
        [OneTimeSetUp]
        public void Init()
        {
            CreateRoles();
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        public async Task GetRoles_ShouldReturnRoleCorrectly(TestCaseData data)
        {
            //Arrange
            var sut = new RoleRepository(Db, Mapper);

            //Act
            var Roles = await sut.GetRoleByIdAsync(data.Id, CancellationToken.None);

            //Assert
            Roles.Should().BeEquivalentTo(data.Expected, opts => opts.Excluding(x => x.Id));
        }

        public class TestCaseData
        {
            public int Id { get; set; }
            public Role? Expected { get; set; }
        }

        private static IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData
                {
                    Id = 0,
                    Expected = null
                };
                yield return new TestCaseData
                {
                    Id = 1,
                    Expected = new()
                    {
                        Code = "Moderator"
                    }
                };
            }
        }
    }
}
