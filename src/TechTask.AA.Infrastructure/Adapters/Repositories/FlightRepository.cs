using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechTask.AA.Core.Models;
using TechTask.AA.Core.Ports.Repositories;
using TechTask.AA.DAL;
using TechTask.AA.DAL.DAO;

namespace TechTask.AA.Infrastructure.Adapters.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly TechTaskDbContext _dbContext;
        private readonly IMapper _mapper;

        public FlightRepository(TechTaskDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Flight[]> GetFlightsByOriginAndDestinationAsync(string? origin, string? destination, CancellationToken cancellationToken)
        {
            var flights = await _dbContext.Flights.AsNoTracking().Where(f =>
                    (origin == null || f.Origin == origin)
                    && (destination == null || f.Destination == destination))
                .OrderBy(f => f.Arrival)
                .Select(f => _mapper.Map<Flight>(f))
                .ToArrayAsync(cancellationToken);

            return flights;
        }

        public async Task<Flight?> GetFlightByIdAsync(int id, CancellationToken cancellationToken)
        {
            var flight = await _dbContext.Flights.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (flight == null) return null;

            return _mapper.Map<Flight>(flight);
        }

        public async Task<Flight> SaveFlightAsync(Flight model, CancellationToken cancellationToken)
        {
            var flight = _mapper.Map<FlightDao>(model);

            if (flight.Id == 0)
            {
                _dbContext.Flights.Add(flight);
            }
            else
            {
                _dbContext.Flights.Update(flight);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<Flight>(flight);

            return result;
        }
    }
}
