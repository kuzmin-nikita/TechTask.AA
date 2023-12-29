using Microsoft.Extensions.Caching.Memory;
using TechTask.AA.Core.Models;
using TechTask.AA.Core.Ports.Repositories;

namespace TechTask.AA.Infrastructure.Adapters.CachedRepositories
{
    public class CachedUserRepository : IUserRepository
    {
        private readonly IUserRepository _repository;
        private readonly IMemoryCache _memoryCache;

        public CachedUserRepository(IUserRepository repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public Task<User?> GetUserByUsernameAsync(string? username, CancellationToken cancellationToken)
        {
            string key = $"user-{username}";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                return _repository.GetUserByUsernameAsync(username, cancellationToken);
            });
        }
    }
}
