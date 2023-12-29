using TechTask.AA.Core.Models;

namespace TechTask.AA.Core.Ports.Repositories;

public interface IFlightRepository
{
    public Task<Flight[]> GetFlightsByOriginAndDestinationAsync(string? origin, string? destination, CancellationToken cancellationToken);
    public Task<Flight?> GetFlightByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Flight> SaveFlightAsync(Flight model, CancellationToken cancellationToken);
}
