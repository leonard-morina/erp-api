using Erp.Api.Authentication;
using Erp.Api.Constants;
using Microsoft.OpenApi.Models;

namespace Erp.Api.Configuration;

public static class ApiServicesConfiguration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenConfiguration =
            configuration.GetSection(ConfigurationConstants.TOKEN).Get<TokenConfiguration>();
        services.AddSingleton<ITokenGenerator>(serviceProvider => new JwtTokenGenerator(tokenConfiguration));
        
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
        });

        var info = new OpenApiInfo
        {
            Version = "v1",
            Title = "Erp Api Documentation",
        };

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", info);

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Description = "Please insert JWT token into field"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        return services;
    }
}