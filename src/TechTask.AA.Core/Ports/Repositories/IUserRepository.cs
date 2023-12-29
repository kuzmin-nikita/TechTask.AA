using TechTask.AA.Core.Models;

namespace TechTask.AA.Core.Ports.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserByUsernameAsync(string? username, CancellationToken cancellationToken);
}
