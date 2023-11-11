using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Core.Common.Cache;

public class DistributedCache<T> : IDistributedCache<T>
{
    private readonly IDistributedCache _distributedCache;

    private readonly string _cacheKeyPrefix;

    public DistributedCache(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
        _cacheKeyPrefix = $"{typeof(T).Namespace}_{typeof(T).Name}_";
    }

    public async Task<(bool Found, T Value)> TryGetValueAsync(string key)
    {
        var value = await GetAsync(key);

        return (value != null, value);
    }

    public async Task<T> GetAsync(string key)
    {
        var cachedResult = await _distributedCache.GetStringAsync(CacheKey(key));

        return cachedResult == null ? default : DeserializeFromString(cachedResult);
    }

    public async Task SetAsync(string key, T item, int secondsToCache)
    {
        var cacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(secondsToCache)
        };

        var serialisedItemToCache = SerialiseForCaching(item);

        await _distributedCache.SetStringAsync(CacheKey(key), serialisedItemToCache, cacheEntryOptions);
    }

    public Task RemoveAsync(string key) => _distributedCache.RemoveAsync(CacheKey(key));

    private string CacheKey(string key) => $"{_cacheKeyPrefix}{key}";

    private static T DeserializeFromString(string cachedResult)
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
            return default;
        }
    }

    private string SerialiseForCaching(T item)
    {
        if (item == null) return null;

        return JsonConvert.SerializeObject(item);
    }
}