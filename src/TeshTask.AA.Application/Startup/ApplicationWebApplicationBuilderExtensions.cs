using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TechTask.AA.Application.Startup
{
    public static class ApplicationWebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddMediatRFeature(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            applicationBuilder.Services.AddFluentValidation(new List<Assembly>() { Assembly.GetExecutingAssembly() });

            return applicationBuilder;
        }
    }
}
