using Erp.Api.Authentication;
using Erp.Api.Cache;
using Erp.Api.Constants;
using Erp.Api.Files;
using Microsoft.OpenApi.Models;

namespace Erp.Api.Configuration;

public static class ApiServicesConfiguration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenConfiguration =
            configuration.GetSection(ConfigurationConstants.TOKEN).Get<TokenConfiguration>();
        services.Configure<TokenConfiguration>(configuration.GetSection(ConfigurationConstants.TOKEN));

        services.AddSingleton(services => tokenConfiguration);
        services.AddSingleton<ITokenGenerator>(serviceProvider => new JwtTokenGenerator(tokenConfiguration));

        var s3BucketConfiguration =
            configuration.GetSection(ConfigurationConstants.S3_BUCKET).Get<S3BucketConfiguration>();
        services.AddSingleton<IFileUploader>(serviceProvider => new AwsS3Service(s3BucketConfiguration));
        services.AddSingleton(serviceProvider => s3BucketConfiguration.Folders);

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConfiguration(configuration.GetSection(ConfigurationConstants.LOGGING));
        });

        var redisConfiguration = configuration.GetSection(ConfigurationConstants.REDIS_CACHE)
            .Get<RedisCacheConfiguration>();
        services.AddSingleton(redisConfiguration);

        if (redisConfiguration.Enabled)
        {
            services.AddStackExchangeRedisCache(options => options.Configuration = redisConfiguration.ConnectionString);
            services.AddSingleton<IResponseCacheService, RedisResponseCacheService>();
        }
        
        

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