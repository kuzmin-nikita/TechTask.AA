using AutoMapper;
using TechTask.AA.Core.Models;
using TechTask.AA.Application.Commands;
using TechTask.AA.Application.Queries;

namespace TechTask.AA.Application.Profiles
{
    public class FlightProfiles : Profile
    {
        public FlightProfiles()
        {
            CreateMap<Flight, CreateFlightCommand.Result>()
                .ForCtorParam(nameof(CreateFlightCommand.Result.Id), opt => opt.MapFrom(src => src.Id))
                .ForCtorParam(nameof(CreateFlightCommand.Result.Origin), opt => opt.MapFrom(src => src.Origin))
                .ForCtorParam(nameof(CreateFlightCommand.Result.Destination), opt => opt.MapFrom(src => src.Destination))
                .ForCtorParam(nameof(CreateFlightCommand.Result.Departure), opt => opt.MapFrom(src => src.Departure))
                .ForCtorParam(nameof(CreateFlightCommand.Result.Arrival), opt => opt.MapFrom(src => src.Arrival))
                .ForCtorParam(nameof(CreateFlightCommand.Result.Status), opt => opt.MapFrom(src => src.Status));

            CreateMap<Flight, UpdateFlightStatusCommand.Result>()
                .ForCtorParam(nameof(UpdateFlightStatusCommand.Result.Id), opt => opt.MapFrom(src => src.Id))
                .ForCtorParam(nameof(UpdateFlightStatusCommand.Result.Origin), opt => opt.MapFrom(src => src.Origin))
                .ForCtorParam(nameof(UpdateFlightStatusCommand.Result.Destination), opt => opt.MapFrom(src => src.Destination))
                .ForCtorParam(nameof(UpdateFlightStatusCommand.Result.Departure), opt => opt.MapFrom(src => src.Departure))
                .ForCtorParam(nameof(UpdateFlightStatusCommand.Result.Arrival), opt => opt.MapFrom(src => src.Arrival))
                .ForCtorParam(nameof(UpdateFlightStatusCommand.Result.Status), opt => opt.MapFrom(src => src.Status));

            CreateMap<Flight, GetFlightsQuery.Result>()
                .ForCtorParam(nameof(GetFlightsQuery.Result.Id), opt => opt.MapFrom(src => src.Id))
                .ForCtorParam(nameof(GetFlightsQuery.Result.Origin), opt => opt.MapFrom(src => src.Origin))
                .ForCtorParam(nameof(GetFlightsQuery.Result.Destination), opt => opt.MapFrom(src => src.Destination))
                .ForCtorParam(nameof(GetFlightsQuery.Result.Departure), opt => opt.MapFrom(src => src.Departure))
                .ForCtorParam(nameof(GetFlightsQuery.Result.Arrival), opt => opt.MapFrom(src => src.Arrival))
                .ForCtorParam(nameof(GetFlightsQuery.Result.Status), opt => opt.MapFrom(src => src.Status));
        }
    }
}
