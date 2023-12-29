using TechTask.AA.Core.Common;

namespace TechTask.AA.API.DTO
{
    /// <summary>
    /// DTO with all the information about flight
    /// </summary>
    /// <param name="Id">Flight Id</param>
    /// <param name="Origin">Origin airport</param>
    /// <param name="Destination">Destination airport</param>
    /// <param name="Departure">Departure time (timezone by origin airport)</param>
    /// <param name="Arrival">Arrival time (timezone by destination airport)</param>
    /// <param name="Status">Flight status (InTime, Delayed, Cancelled)</param>
    public sealed record FlightDto(
        int Id,
        string Origin,
        string Destination,
        DateTimeOffset Departure,
        DateTimeOffset Arrival,
        FlightStatus Status);

    /// <summary>
    /// DTO for getting all the flights filtered by Origin and/or Destination
    /// </summary>
    /// <param name="Origin">Origin airport</param>
    /// <param name="Destination">Destination airport</param>
    public sealed record GetFlightsDto(string? Origin, string? Destination);

    /// <summary>
    /// DTO for flight creation
    /// </summary>
    /// <param name="Origin">Origin airport</param>
    /// <param name="Destination">Destination airport</param>
    /// <param name="Departure">Departure time (timezone by origin airport)</param>
    /// <param name="Arrival">Arrival time (timezone by destination airport)</param>
    public sealed record CreateFlightDto(
        string Origin,
        string Destination,
        DateTimeOffset Departure,
        DateTimeOffset Arrival);

    /// <summary>
    /// DTO for flight status updating
    /// </summary>
    /// <param name="Id">Flight Id</param>
    /// <param name="Status">Flight status (InTime, Delayed, Cancelled)</param>
    public sealed record UpdateFlightStatusDto(
        int Id,
        FlightStatus Status);
}
