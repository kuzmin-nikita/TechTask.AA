using AutoMapper;
using TechTask.AA.API.DTO;
using TechTask.AA.Application.Commands;
using TechTask.AA.Application.Queries;

namespace TeshTask.API.Profiles
{
    public class FlightProfiles : Profile
    {
        public FlightProfiles()
        {
            CreateMap<GetFlightsDto, GetFlightsQuery>();
            CreateMap<GetFlightsQuery.Result, FlightDto>();

            CreateMap<CreateFlightDto, CreateFlightCommand>();
            CreateMap<CreateFlightCommand.Result, FlightDto>();

            CreateMap<UpdateFlightStatusDto, UpdateFlightStatusCommand>();
            CreateMap<UpdateFlightStatusCommand.Result, FlightDto>();
        }
    }
}
