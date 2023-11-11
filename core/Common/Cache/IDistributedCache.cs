using System.Threading.Tasks;

namespace Core.Common.Cache;

public interface IDistributedCache<T>
{
    Task<T> GetAsync(string key);
    Task RemoveAsync(string key);
    Task SetAsync(string key, T item, int secondsToCache);
    Task<(bool Found, T Value)> TryGetValueAsync(string key);
}