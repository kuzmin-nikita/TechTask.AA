using Microsoft.Extensions.Caching.Memory;
using TechTask.AA.Core.Models;
using TechTask.AA.Core.Ports.Repositories;

namespace TechTask.AA.Infrastructure.Adapters.CachedRepositories
{
    public class CachedFlightRepository : IFlightRepository
    {
        private readonly IFlightRepository _repository;
        private readonly IMemoryCache _memoryCache;

        public CachedFlightRepository(IFlightRepository repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public Task<Flight?> GetFlightByIdAsync(int id, CancellationToken cancellationToken)
        {
            string key = $"flight-{id}";

            return _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                return await _repository.GetFlightByIdAsync(id, cancellationToken);
            });
        }

        public Task<Flight[]> GetFlightsByOriginAndDestinationAsync(string? origin, string? destination, CancellationToken cancellationToken)
        {
            string key = $"flight-{origin}-{destination}";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                return _repository.GetFlightsByOriginAndDestinationAsync(origin, destination, cancellationToken);
            });
        }

        public Task<Flight> SaveFlightAsync(Flight model, CancellationToken cancellationToken)
        {
            //Consider removing only entries with model.Origin/model.Destination and null/null keys
            if (_memoryCache is MemoryCache memoryCache)
            {
                memoryCache.Clear();
            }

            string idKey = $"flight-{model.Id}";

            var flight = _memoryCache.Set(idKey, _repository.SaveFlightAsync(model, cancellationToken), TimeSpan.FromMinutes(5));

            _memoryCache.Remove("");

            return flight;
        }
    }
}
