using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechTask.AA.Core.Models;
using TechTask.AA.Core.Ports.Repositories;
using TechTask.AA.DAL;

namespace TechTask.AA.Infrastructure.Adapters.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TechTaskDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoleRepository(TechTaskDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Role?> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
        {
            var role = await _dbContext.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

            if (role == null) return null;

            return _mapper.Map<Role>(role);
        }
    }
}
