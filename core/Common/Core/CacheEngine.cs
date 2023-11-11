using System;
using System.Runtime.Caching;

namespace Core.Common.Core;

public class CacheEngine
{
    private static readonly ObjectCache Cache = MemoryCache.Default;

    public static T Get<T>(string key) where T : class
    {
        try
        {
            return (T)Cache[key];
        }
        catch
        {
            return null;
        }
    }

    public static void Add<T>(string key, T item, int duration) where T : class
    {
        if (item == null)
            return;
        Remove(key);
        if (duration == 0)
        {
            Cache.Add(key, item, DateTimeOffset.Now.AddMinutes(1440));
        }
        else
        {
            var cacheItemPolicy = new CacheItemPolicy();
            Cache.Add(key, item, DateTimeOffset.Now.AddMinutes(duration));
        }
    }

    public static void Remove(string key)
    {
        Cache.Remove(key);
    }
}