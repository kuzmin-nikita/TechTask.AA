using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechTask.AA.DAL.DAO;

namespace TechTask.AA.DAL.Configurations
{
    public class RoleConfiguraton : IEntityTypeConfiguration<RoleDao>
    {
        public void Configure(EntityTypeBuilder<RoleDao> builder)
        {
            builder.Property(x => x.Id);
            builder.Property(x => x.Code).HasMaxLength(256);

            //Primary key
            builder.HasKey(x => x.Id);

            //Unique constraint
            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
