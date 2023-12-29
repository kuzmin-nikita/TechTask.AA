using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechTask.AA.DAL.DAO;

namespace TechTask.AA.DAL.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<FlightDao>
    {
        public void Configure(EntityTypeBuilder<FlightDao> builder)
        {
            builder.Property(a => a.Id);
            builder.Property(x => x.Origin).HasMaxLength(256);
            builder.Property(x => x.Destination).HasMaxLength(256);
            builder.Property(x => x.Departure);
            builder.Property(x => x.Arrival);
            builder.Property(x => x.Status);

            //Primary key
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.Origin, x.Destination });
        }
    }
}
