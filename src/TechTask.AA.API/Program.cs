using Serilog;
using System.Text.Json.Serialization;
using TechTask.AA.API.Middlewares;
using TechTask.AA.Infrastructure.Startup;
using TechTask.AA.Application.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddHttpContextAccessor();

builder.AddSwaggerFeature();

builder.AddDatabaseFeature();
builder.AddRepositoriesFeature();

builder.AddMediatRFeature();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.AddJWTAuthorization();

builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration));

var app = builder.Build();

app.UseSwaggerFeature();

app.MapControllers();
app.UseDatabaseFeature();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseMiddleware<LogUsernameMiddleware>();

app.Run();
