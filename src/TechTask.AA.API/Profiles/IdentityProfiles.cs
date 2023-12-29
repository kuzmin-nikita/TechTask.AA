using AutoMapper;
using TechTask.AA.API.DTO;
using TechTask.AA.Application.Commands;

namespace TechTask.AA.API.Profiles
{
    public class IdentityProfiles : Profile
    {
        public IdentityProfiles()
        {
            CreateMap<AuthorizeDto, AuthorizeCommand>();
            CreateMap<AuthorizeCommand.Result, IdentityDto>();
        }
    }
}
