namespace Erp.Api.Cache;

public interface IResponseCacheService
{
    Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
    Task<string> GetCachedResponseAsync(string cacheKey);
    Task DeleteAllValuesByIncludedCacheKeyStringAsync(string[] keyPatterns);
}