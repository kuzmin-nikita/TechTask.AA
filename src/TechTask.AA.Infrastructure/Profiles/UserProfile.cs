using AutoMapper;
using TechTask.AA.Core.Models;
using TechTask.AA.DAL.DAO;

namespace TechTask.AA.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDao>().ReverseMap();
        }
    }
}
