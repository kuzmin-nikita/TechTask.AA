using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechTask.AA.Core.Ports.Repositories;
using TechTask.AA.DAL;
using TechTask.AA.Infrastructure.Adapters.CachedRepositories;
using TechTask.AA.Infrastructure.Adapters.Repositories;

namespace TechTask.AA.Infrastructure.Startup
{
    public static class InfrastructureWebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddDatabaseFeature(this WebApplicationBuilder applicationBuilder)
        {
            var dbConnectionString = applicationBuilder.Configuration.GetConnectionString("DefaultDatabase");

            void Options(DbContextOptionsBuilder o)
            {
                o.UseNpgsql(dbConnectionString);
                //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }

            applicationBuilder.Services
                .AddDbContext<TechTaskDbContext>(Options, optionsLifetime: ServiceLifetime.Singleton)
                .AddDbContextFactory<TechTaskDbContext>(Options);

            return applicationBuilder;
        }

        public static WebApplicationBuilder AddRepositoriesFeature(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddRepositories();
            applicationBuilder.Services.AddCachedRepositories();

            return applicationBuilder;
        }

        private static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFlightRepository, FlightRepository>();
            serviceCollection.AddScoped<IRoleRepository, RoleRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddCachedRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();

            serviceCollection.Decorate<IFlightRepository, CachedFlightRepository>();
            serviceCollection.Decorate<IRoleRepository, CachedRoleRepository>();
            serviceCollection.Decorate<IUserRepository, CachedUserRepository>();
        }
    }
}
