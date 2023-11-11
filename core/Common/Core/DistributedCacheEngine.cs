using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Core.Common.Core;

public class DistributedCacheEngine
{
    private readonly IDistributedCache _distributedCache;

    public DistributedCacheEngine(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T> GetAsync<T>(string key) where T : class
    {
        var cachedResult = await _distributedCache.GetStringAsync(GetCacheKey<T>(key));

        return cachedResult == null ? default : DeserialiseFromString<T>(cachedResult);
    }

    private static T DeserialiseFromString<T>(string cachedResult) where T : class
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(cachedResult, new JsonSerializerSettings
            {
                MaxDepth = 10
            });
        }
        catch (Exception)
        {
            //_logger.LogError(ex, "Failed to deserialise from cached string");
            return default;
        }
    }

    private static string GetCacheKey<T>(string key)
    {
        var cacheKeyPrefix = $"{typeof(T).Namespace}_{typeof(T).Name}_";
        return $"{cacheKeyPrefix}{key}";
    }
}