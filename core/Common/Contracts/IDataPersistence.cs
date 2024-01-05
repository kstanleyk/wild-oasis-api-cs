using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Common.Helpers;

namespace Core.Common.Contracts;

public interface IDataPersistence<TEntity> : IDisposable, IDataPersistenceBase where TEntity : class
{
    Task<TEntity> GetAsync(TEntity entity);
    Task<TEntity> GetAsync(string entity);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
    Task<TEntity[]> GetAllAsync();
    Task<TEntity[]> GetAllAsync(int page, int count);
    Task<int> GetCountAsync();
    Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where);
    Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, string>> orderBy);
    Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, string>> orderBy, Expression<Func<TEntity, string>> thenBy);
    Task<RepositoryActionResult<TEntity>> AddAsync(TEntity entity);
    Task<RepositoryActionResult<IEnumerable<TEntity>>> AddManyAsync(IEnumerable<TEntity> entities);
    Task<RepositoryActionResult<TEntity>> EditAsync(TEntity entity);
    Task<RepositoryActionResult<TEntity>> DeleteAsync(TEntity entity);
    Task<RepositoryActionResult<TEntity>> DeleteManyAsync(Expression<Func<TEntity, bool>> where);
    Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> where);
    Task<TEntity> GetFirstOrDefaultAsync();
    Task<RepositoryActionResult<IEnumerable<TEntity>>> EditManyAsync(IEnumerable<TEntity> entities);
}