
using Erp.Core.Interfaces;
using Erp.Infrastructure.Data;
using Erp.Infrastructure.Logging;

namespace Erp.Api.Configuration;

public static class CoreServicesConfiguration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IAsyncRepository<>), typeof(ErpRepository<>));
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        return services;
    }
}