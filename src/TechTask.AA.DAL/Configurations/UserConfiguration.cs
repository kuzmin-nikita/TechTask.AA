using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechTask.AA.DAL.DAO;

namespace TechTask.AA.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDao>
    {
        public void Configure(EntityTypeBuilder<UserDao> builder)
        {
            builder.Property(a => a.Id);
            builder.Property(x => x.Username).HasMaxLength(256);
            builder.Property(x => x.Password).HasMaxLength(256);
            builder.Property(x => x.RoleId);

            //Primary key
            builder.HasKey(x => x.Id);

            //Unique constraint
            builder.HasIndex(x => x.Username).IsUnique();

            //Foreign key
            builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId);
        }
    }
}
