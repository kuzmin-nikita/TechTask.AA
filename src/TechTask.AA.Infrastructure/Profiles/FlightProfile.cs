using AutoMapper;
using TechTask.AA.Core.Models;
using TechTask.AA.DAL.DAO;

namespace TechTask.AA.Infrastructure.Profiles
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<Flight, FlightDao>().ReverseMap();
        }
    }
}
