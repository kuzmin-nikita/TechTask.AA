using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechTask.AA.Core.Models;
using TechTask.AA.Core.Ports.Repositories;
using TechTask.AA.DAL;

namespace TechTask.AA.Infrastructure.Adapters.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TechTaskDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(TechTaskDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User?> GetUserByUsernameAsync(string? username, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

            if (user == null) return null;

            return _mapper.Map<User>(user);
        }
    }
}
