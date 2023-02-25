using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Erp.Api.Cache;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

public class RemoveCacheKeysOnSuccessAttribute : Attribute, IAsyncActionFilter 
{
    private readonly string[] _cacheKeyPatterns;

    public RemoveCacheKeysOnSuccessAttribute(params string[] cacheKeyPatterns)
    {
        _cacheKeyPatterns = cacheKeyPatterns;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisCacheConfiguration>();
        if (!cacheConfiguration.Enabled)
        {
            await next();
            return;
        }

        var executedContext = await next();
        if (executedContext is OkObjectResult)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            await cacheService.DeleteAllValuesByIncludedCacheKeyStringAsync(_cacheKeyPatterns);
        }
    }
}