using AutoMapper;
using FluentValidation;
using MediatR;
using TechTask.AA.Core.Common;
using TechTask.AA.Core.Ports.Repositories;

namespace TechTask.AA.Application.Queries
{
    public record GetFlightsQuery(
        string? Origin,
        string? Destination) : IRequest<GetFlightsQuery.Result[]>
    {
        public record Result(
            int Id,
            string Origin,
            string Destination,
            DateTimeOffset Departure,
            DateTimeOffset Arrival,
            FlightStatus Status);

        public class Validator : AbstractValidator<GetFlightsQuery>
        {
            public Validator()
            {
                RuleFor(d => d.Origin).MaximumLength(256);
                RuleFor(d => d.Destination).MaximumLength(256);
            }
        }

        public class Handler : IRequestHandler<GetFlightsQuery, Result[]>
        {
            private readonly IFlightRepository _repository;
            private readonly IMapper _mapper;

            public Handler(IFlightRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result[]> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
            {
                var flights = await _repository.GetFlightsByOriginAndDestinationAsync(request.Origin, request.Destination, cancellationToken);

                var result = flights.Select(f => _mapper.Map<Result>(f)).ToArray();

                return result;
            }
        }
    }
}
