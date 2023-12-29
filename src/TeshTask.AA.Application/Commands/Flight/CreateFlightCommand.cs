using AutoMapper;
using FluentValidation;
using MediatR;
using TechTask.AA.Core.Common;
using TechTask.AA.Core.Models;
using TechTask.AA.Core.Ports.Repositories;

namespace TechTask.AA.Application.Commands
{
    public record CreateFlightCommand(
        string Origin,
        string Destination,
        DateTimeOffset Departure,
        DateTimeOffset Arrival) : IRequest<CreateFlightCommand.Result>
    {
        public record Result(
            int Id,
            string Origin,
            string Destination,
            DateTimeOffset Departure,
            DateTimeOffset Arrival,
            FlightStatus Status);

        public class Validator : AbstractValidator<CreateFlightCommand>
        {
            public Validator()
            {
                RuleFor(d => d.Origin).NotNull().Length(1, 256);
                RuleFor(d => d.Destination).NotNull().Length(1, 256);
                RuleFor(d => d.Departure).NotEmpty();
                RuleFor(d => d.Arrival).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<CreateFlightCommand, Result>
        {
            private readonly IFlightRepository _repository;
            private readonly IMapper _mapper;

            public Handler(IFlightRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
            {
                var model = new Flight(request.Origin, request.Destination, request.Departure, request.Arrival);

                var flight = await _repository.SaveFlightAsync(model, cancellationToken);

                var result = _mapper.Map<Result>(flight);

                return result;
            }
        }
    }
}
