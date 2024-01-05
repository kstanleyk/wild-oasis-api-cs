using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.Helpers;
using Microsoft.Extensions.Caching.Distributed;
using WildOasis.Domain.Contracts.Service;

namespace WildOasis.Application;

public abstract class ServiceBase<TEntity, TModel> : DomainBase, IServiceBase<TModel> 
    where TEntity : class, IIdentifiableEntity, new()
    where TModel : class, IEntityBase, new()
{
    private readonly IDataPersistence<TEntity> _persistence;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;

    protected ServiceBase(IDataPersistence<TEntity> persistence, IMapper mapper, IDistributedCache cache = null)
    {
        _persistence = persistence;
        _mapper = mapper;
        _cache = cache;
    }

    public virtual async Task<TModel[]> GetAllAsync(bool bypassCache = false)
    {
        const string key = $"urn:{nameof(TModel)}";

        TModel[] models;

        //if (_cache == null)
        //    return await GetItemsAsync();

        //var cachedData = await _cache.GetAsync(key);
        //if (cachedData != null)
        //{
        //    var cachedDataString = Encoding.UTF8.GetString(cachedData);
        //    models = JsonSerializer.Deserialize<TModel[]>(cachedDataString);
        //}
        //else
        //{
        //    models = await GetItemsAsync();

        //    var cachedDataString = JsonSerializer.Serialize(models);
        //    var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

        //    var options = new DistributedCacheEntryOptions()
        //        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
        //        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

        //    await _cache.SetAsync(key, dataToCache, options);
        //}

        models = await GetItemsAsync();

        return models;
    }

    private async Task<TModel[]> GetItemsAsync()
    {
        var entities = await _persistence.GetAllAsync();
        var models = _mapper.Map<TModel[]>(entities);
        return models;
    }

    public virtual async Task<TModel> GetAsync(string code)
    {
        var entity = await _persistence.GetAsync(code);
        return _mapper.Map<TModel>(entity);
    }

    public virtual async Task<RepositoryActionResult<TModel>> AddAsync(TModel model)
    {
        var actionResult = await _persistence.AddAsync(_mapper.Map<TEntity>(model));
        return GetRepositoryActionResult(model,
            () => new RepositoryActionResult<TModel>(_mapper.Map<TModel>(actionResult.Entity), actionResult.Status));
    }

    public virtual async Task<RepositoryActionResult<TModel[]>> AddManyAsync(TModel[] models)
    {
        var actionResult = await _persistence.AddManyAsync(_mapper.Map<TEntity[]>(models));
        return new RepositoryActionResult<TModel[]>(_mapper.Map<TModel[]>(actionResult.Entity),
            actionResult.Status);
    }

    public virtual async Task<RepositoryActionResult<TModel>> EditAsync(TModel model)
    {
        var actionResult = await _persistence.EditAsync(_mapper.Map<TEntity>(model));
        return GetRepositoryActionResult(model,
            () => new RepositoryActionResult<TModel>(_mapper.Map<TModel>(actionResult.Entity), actionResult.Status));
    }

    public virtual async Task<RepositoryActionResult<TModel>> DeleteAsync(TModel model)
    {
        var actionResult = await _persistence.DeleteAsync(_mapper.Map<TEntity>(model));
        return GetRepositoryActionResult(model,
            () => new RepositoryActionResult<TModel>(_mapper.Map<TModel>(actionResult.Entity), actionResult.Status));
    }

    public RepositoryActionResult<TModel> GetRepositoryActionResult(TModel value,
        Func<RepositoryActionResult<TModel>> codeToExecute)
    {
        try
        {
            return codeToExecute.Invoke();
        }
        catch (Exception ex)
        {
            return new RepositoryActionResult<TModel>(null, RepositoryActionStatus.Error, ex);
        }
    }

    public async Task<TModel[]> GetAllAsync(bool bypassCache, int page, int count)
    {
        const string key = $"urn:{nameof(TModel)}";

        if (!bypassCache)
        {
            var rtn = CacheEngine.Get<TModel[]>(key);
            if (rtn != null) return rtn.Skip(page*count).Take(count).ToArray();
        }

        var models = await _persistence.GetAllAsync(page, count);
        var result = _mapper.Map<TModel[]>(models);

        CacheEngine.Add(key, result, 0);

        return result;
    }

    public async Task<int> GetCount(bool bypassCache)
    {
        const string key = $"urn:{nameof(TModel)}";

        if (bypassCache) return await _persistence.GetCountAsync();
        var rtn = CacheEngine.Get<TModel[]>(key);
        if (rtn != null) return rtn.Count();

        return await _persistence.GetCountAsync(); 
    }

    protected override void DisposeCore()
    {
        _persistence?.Dispose();
    }
}