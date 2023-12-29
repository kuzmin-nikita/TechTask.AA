using TechTask.AA.Core.Models;

namespace TechTask.AA.Core.Ports.Repositories;

public interface IRoleRepository
{
    public Task<Role?> GetRoleByIdAsync(int id, CancellationToken cancellationToken);
}
