using TechTask.AA.Core.Common;
using TechTask.AA.Core.Exceptions;

namespace TechTask.AA.Core.Models;

public class Flight
{
    public int Id { get; set; }
    public string Origin { get; set; } = null!;
    public string Destination { get; set; } = null!;
    public DateTimeOffset Departure { get; set; }
    public DateTimeOffset Arrival { get; set; }
    public FlightStatus Status { get; set; }

    public Flight()
    {
    }

    public Flight(string origin, string destination, DateTimeOffset departure, DateTimeOffset arrival)
    {
        if (origin == destination)
        {
            throw new BadRequestException("Origin city cannot be same as destination city");
        }

        if (departure.ToUniversalTime() >= arrival.ToUniversalTime())
        {
            throw new BadRequestException("Departure time must be less than arrival time");
        }

        Origin = origin;
        Destination = destination;
        Departure = departure;
        Arrival = arrival;
        Status = FlightStatus.InTime;
    }

    public void UpdateStatus(FlightStatus status)
    {
        if (status < Status)
        {
            throw new BadRequestException($"Flight status '{status}' cannot be applied to flight with status '{Status}'");
        }

        if (status == Status)
        {
            return;
        }

        Status = status;
    }
}
