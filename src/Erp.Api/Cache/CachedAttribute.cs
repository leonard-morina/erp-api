using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Erp.Api.Cache;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CachedAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _timeToLiveSeconds;
    private readonly bool _returnedDataIsDependentOnUserToken;
    private readonly IResponseCacheService _responseCacheService;

    public CachedAttribute(int timeToLiveSeconds, bool returnedDataIsDependentOnUserToken = false)
    {
        _timeToLiveSeconds = timeToLiveSeconds;
        _returnedDataIsDependentOnUserToken = returnedDataIsDependentOnUserToken;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //before the request is processed
        var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisCacheConfiguration>();
        if (!cacheConfiguration.Enabled)
        {
            await next();
            return;
        }

        var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
        string userToken = "";
        var request = context.HttpContext.Request;
        if (_returnedDataIsDependentOnUserToken)
        {
            try
            {
                var token = request.Headers["Authorization"].FirstOrDefault()?.Split().Last();
                if (!string.IsNullOrEmpty(token))
                {
                    userToken = token;
                }
            }
            catch (Exception ex)
            {
                //ignored
            }
        }
        var cacheKey = GenerateCacheKeyFromRequest(request, userToken);
        var cachedResponse = await cacheService.GetCachedResponseAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedResponse))
        {
            var contentResult = new ContentResult
            {
                Content = cachedResponse,
                ContentType = "application/json",
                StatusCode = 200
            };
            context.Result = contentResult;
            return;
        }
        
        var executedContext = await next();
        //after the request has a response
        if (executedContext.Result is OkObjectResult okObjectResult)
        {
            await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value,
                TimeSpan.FromSeconds(_timeToLiveSeconds));
        }
    }

    private static string GenerateCacheKeyFromRequest(HttpRequest request, string userToken)
    {
        var keyBuilder = new StringBuilder();
        keyBuilder.Append($"{request.Path}");

        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            keyBuilder.Append($"{key}-{value}");
        }

        if (!string.IsNullOrEmpty(userToken))
        {
            keyBuilder.Append($"{userToken}");
        }

        return keyBuilder.ToString();
    }
}