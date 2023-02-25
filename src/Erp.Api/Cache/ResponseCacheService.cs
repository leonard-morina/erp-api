using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Erp.Api.Cache;

public class ResponseCacheService : IResponseCacheService
{
    private readonly IDistributedCache _distributedCache;

    public ResponseCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
    {
        if (response == null) return;
        var serializedResponse = JsonSerializer.Serialize(response);
        await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = timeToLive
        });
    }

    private async Task DeleteValueByKey(string cacheKey)
    {
        await _distributedCache.RemoveAsync(cacheKey);
    }

    public async Task DeleteAllValuesByIncludedCacheKeyStringAsync(string[] cacheKeyPatterns)
    {
        var redisDatabase = _distributedCache as IDatabase;
        foreach (var keyPattern in cacheKeyPatterns)
        {
            var keys = redisDatabase?.Execute("SCAN", "0", "MATCH", keyPattern);
            foreach (var (key, _) in keys.ToDictionary())
            {
                DeleteValueByKey(key);
            }
        }
    }

    public async Task<string> GetCachedResponseAsync(string cacheKey)
    {
        var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);
        return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
    }
}