using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TechTask.AA.Core.Helpers;

namespace TechTask.AA.Infrastructure.Startup
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddSwaggerFeature(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddEndpointsApiExplorer();
            applicationBuilder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TechTask API",
                    Description = "TechTask API for getting, creating and changing flights"
                });
                config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                config.MapType<DateTimeOffset>(() =>
                    new OpenApiSchema
                    {
                        Type = "string",
                        Format = "date-time",
                        Example = new OpenApiString(DateTimeOffset.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mmK"))
                    }
                );
            });

            return applicationBuilder;
        }

        public static WebApplicationBuilder AddJWTAuthorization(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddAuthorization();
            applicationBuilder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = JWTHelper.Constants.Issuer,
                    ValidAudience = JWTHelper.Constants.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTHelper.Constants.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            return applicationBuilder;
        }
    }
}
