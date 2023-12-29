namespace TechTask.AA.Infrastructure.Startup
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseSwaggerFeature(this WebApplication application)
        {
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "TechTask API");
                });
            }

            return application;
        }
    }
}
