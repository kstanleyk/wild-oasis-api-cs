using System;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Helpers;

namespace WildOasis.Domain.Contracts.Service;

public interface IServiceBase<TModel> : IDisposable where TModel : class, IEntityBase, new()
{
    Task<TModel[]> GetAllAsync(bool bypassCache);

    Task<int> GetCount(bool byPassCache);
    Task<TModel[]> GetAllAsync(bool byPassCache, int page, int count);
    Task<RepositoryActionResult<TModel>> AddAsync(TModel model);
    Task<RepositoryActionResult<TModel>> EditAsync(TModel model);
    Task<RepositoryActionResult<TModel>> DeleteAsync(TModel model);

    Task<TModel> GetAsync(string tenant, string code);
}