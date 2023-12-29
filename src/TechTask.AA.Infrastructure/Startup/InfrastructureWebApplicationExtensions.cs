using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechTaskDbContext = TechTask.AA.DAL.TechTaskDbContext;

namespace TechTask.AA.Infrastructure.Startup
{
    public static class InfrastructureWebApplicationExtensions
    {
        public static WebApplication UseDatabaseFeature(this WebApplication application)
        {
            var serviceScopeFactory = application.Services.GetRequiredService<IServiceScopeFactory>();

            using var scope = serviceScopeFactory.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<TechTaskDbContext>();
            ctx.Database.Migrate();

            return application;
        }
    }
}
