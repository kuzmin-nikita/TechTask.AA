using TechTask.AA.Core.Common;

namespace TechTask.AA.DAL.DAO
{
    public class FlightDao
    {
        public int Id { get; set; }
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        //Got some troubles with storaging DateTimeOffset
        //Npgsql is saving it as "timestamp with time zone" but in UTC with no timezone
        //Consider adding column with timezone or modify Origin/Detination as Foreign Key to some Location entity
        public DateTimeOffset Departure { get; set; }
        public DateTimeOffset Arrival { get; set; }
        public FlightStatus Status { get; set; }
    }
}
