using AutoMapper;
using NUnit.Framework;
using TechTask.AA.API.DTO;
using TechTask.AA.API.Profiles;
using TechTask.AA.Application.Commands;

namespace TechTask.AA.API.Tests.Profiles
{
    public class IdentityProfilesTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<IdentityProfiles>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public void CreateMap_AuthorizeDto_AuthorizeCommand()
        {
            //Arrange
            var dto = new AuthorizeDto(
                Username: "Username",
                Password: "Password");
            var expectedCommand = new AuthorizeCommand(
                Username: "Username",
                Password: "Password");

            //Act
            var actual = _mapper.Map<AuthorizeCommand>(dto);

            //Assert
            Assert.AreEqual(expectedCommand, actual);
        }

        [Test]
        public void CreateMap_AuthorizeCommandResult_IdentityDto()
        {
            //Arrange
            var result = new AuthorizeCommand.Result(Token: "string");
            var expectedDto = new IdentityDto(Token: "string");

            //Act
            var actual = _mapper.Map<IdentityDto>(result);

            //Assert
            Assert.AreEqual(expectedDto, actual);
        }
    }
}
