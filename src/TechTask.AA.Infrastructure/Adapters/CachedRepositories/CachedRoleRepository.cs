using Microsoft.Extensions.Caching.Memory;
using TechTask.AA.Core.Models;
using TechTask.AA.Core.Ports.Repositories;

namespace TechTask.AA.Infrastructure.Adapters.CachedRepositories
{
    public class CachedRoleRepository : IRoleRepository
    {
        private readonly IRoleRepository _repository;
        private readonly IMemoryCache _memoryCache;

        public CachedRoleRepository(IRoleRepository repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public Task<Role?> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
        {
            string key = $"role-{id}";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                return _repository.GetRoleByIdAsync(id, cancellationToken);
            });
        }
    }
}
