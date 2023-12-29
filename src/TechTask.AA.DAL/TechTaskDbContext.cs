using Microsoft.EntityFrameworkCore;
using TechTask.AA.Core.Helpers;
using TechTask.AA.DAL.DAO;

namespace TechTask.AA.DAL
{
    public class TechTaskDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TechTaskDbContext()
        {
        }

        public TechTaskDbContext(DbContextOptions<TechTaskDbContext> options)
            : base(options)
        {
        }

        public DbSet<FlightDao> Flights { get; set; } = null!;
        public DbSet<UserDao> Users { get; set; } = null!;
        public DbSet<RoleDao> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleDao>().HasData(
                   new RoleDao() { Id = 1, Code = "Moderator" },
                   new RoleDao() { Id = 2, Code = "User" }
            );
            modelBuilder.Entity<UserDao>().HasData(
                   new UserDao() { Id = 1, Username = "moderator", Password = HashHelper.ComputeHash("moderator"), RoleId = 1 },
                   new UserDao() { Id = 2, Username = "user", Password = HashHelper.ComputeHash("user"), RoleId = 2 }
            );
        }
    }
}