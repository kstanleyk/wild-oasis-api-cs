namespace Core.Common.Cache;

public interface IDistributedCacheFactory
{
    IDistributedCache<T> GetCache<T>();
}