using AutoMapper;
using FluentValidation;
using MediatR;
using TechTask.AA.Core.Common;
using TechTask.AA.Core.Exceptions;
using TechTask.AA.Core.Ports.Repositories;

namespace TechTask.AA.Application.Commands
{
    public record UpdateFlightStatusCommand(
        int Id,
        FlightStatus Status) : IRequest<UpdateFlightStatusCommand.Result>
    {
        public record Result(
            int Id,
            string Origin,
            string Destination,
            DateTimeOffset Departure,
            DateTimeOffset Arrival,
            FlightStatus Status);

        public class Validator : AbstractValidator<UpdateFlightStatusCommand>
        {
            public Validator()
            {
                RuleFor(d => d.Id).ExclusiveBetween(0, int.MaxValue);
            }
        }

        public class Handler : IRequestHandler<UpdateFlightStatusCommand, Result>
        {
            private readonly IFlightRepository _repository;
            private readonly IMapper _mapper;

            public Handler(IFlightRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result> Handle(UpdateFlightStatusCommand request, CancellationToken cancellationToken)
            {
                var flight = await _repository.GetFlightByIdAsync(request.Id, cancellationToken);

                if (flight == null)
                {
                    throw new NotFoundException($"Flight with Id: {request.Id} was not found");
                }

                flight.UpdateStatus(request.Status);

                flight = await _repository.SaveFlightAsync(flight, cancellationToken);

                var result = _mapper.Map<Result>(flight);

                return result;
            }
        }
    }
}
