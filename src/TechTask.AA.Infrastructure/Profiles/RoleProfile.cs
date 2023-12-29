using AutoMapper;
using TechTask.AA.Core.Models;
using TechTask.AA.DAL.DAO;

namespace TechTask.AA.Infrastructure.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDao>().ReverseMap();
        }
    }
}
