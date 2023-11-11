using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Common.Data;

public abstract class DataPersistenceBase<TEntity, TContext> : Disposable, IDataPersistence<TEntity>
    where TEntity : class, IIdentifiableEntity, new()
    where TContext : DbContext
{
    protected TContext Context;
    protected IDbConnection Db;

    protected DbSet<TEntity> DbSet { get; }

    protected DataPersistenceBase(TContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<RepositoryActionResult<TEntity>> AddAsync(TEntity entity)
    {
        try
        {
            DbSet.Add(entity);
            var result = await SaveChangesAsync();
            return result != 0
                ? new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.Created)
                : new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.NothingModified);
        }
        catch (Exception ex)
        {
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
        }
    }


    public virtual async Task<RepositoryActionResult<TEntity>> AddAsync(TEntity entity,
        Expression<Func<TEntity, string>> sortOrder, string keySelector, int propertyLength)
    {
        await using var tx = await Context.Database.BeginTransactionAsync();
        try
        {
            var lastEntity = DbSet.OrderByDescending(sortOrder).ToArray().FirstOrDefault();

            var lastCode = string.Empty;


            if (lastEntity == null)
            {
                lastCode = "1";
            }
            else
            {
                var properties = lastEntity.GetType().GetProperties().ToList();
                foreach (var property in properties)
                {
                    if (property.Name != keySelector) continue;
                    var value = property.GetValue(lastEntity, null);
                    lastCode = (value?.ToString().ToNumValue() + 1)
                        .ToNumValue().ToString(CultureInfo.InvariantCulture);
                    break;
                }
            }

            var serial = GetEntityCode(lastCode, propertyLength);
            var targetProperties = entity.GetType().GetProperties().ToList();
            foreach (var property in targetProperties.Where(property => property.Name == keySelector))
            {
                property.SetValue(entity, serial, null);
                break;
            }

            DbSet.Add(entity);

            var result = await SaveChangesAsync();
            if (result == 0)
            {
                await tx.RollbackAsync();
                return new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.NothingModified);
            }

            await tx.CommitAsync();

            return new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.Created);
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync();
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
        }
    }

    //public virtual async Task<RepositoryActionResult<TEntity>> AddAsync(TEntity entity,
    //    Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, string>> sortOrder, string keySelector,
    //    int propertyLength)
    //{
    //    await using var tx = await Context.Database.BeginTransactionAsync();
    //    try
    //    {
    //        var lastEntity = DbSet.Where(filter).OrderByDescending(sortOrder).ToArray().FirstOrDefault();

    //        var lastCode = string.Empty;


    //        if (lastEntity == null)
    //        {
    //            lastCode = "1";
    //        }
    //        else
    //        {
    //            var properties = lastEntity.GetType().GetProperties().ToList();
    //            foreach (var property in properties)
    //            {
    //                if (property.Name != keySelector) continue;
    //                var value = property.GetValue(lastEntity, null);
    //                lastCode = (value?.ToString().ToNumValue() + 1)
    //                    .ToNumValue().ToString(CultureInfo.InvariantCulture);
    //                break;
    //            }
    //        }

    //        var serial = GetEntityCode(lastCode, propertyLength);
    //        var targetProperties = entity.GetType().GetProperties().ToList();
    //        foreach (var property in targetProperties.Where(property => property.Name == keySelector))
    //        {
    //            property.SetValue(entity, serial, null);
    //            break;
    //        }

    //        DbSet.Add(entity);

    //        var result = await SaveChangesAsync();
    //        if (result == 0)
    //        {
    //            await tx.RollbackAsync();
    //            return new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.NothingModified);
    //        }


    //        await tx.CommitAsync();

    //        return new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.Created);
    //    }
    //    catch (Exception ex)
    //    {
    //        await tx.RollbackAsync();
    //        return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
    //    }
    //}


    public virtual async Task<RepositoryActionResult<TEntity>> AddAsync(TEntity entity,
    Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, string>> sortOrder, string keySelector,
    int propertyLength)
    {
        await using var tx = await Context.Database.BeginTransactionAsync();
        try
        {
            var result = await AddAsync(entity, filter, sortOrder, keySelector, propertyLength, tx);
            if (result.Status == RepositoryActionStatus.NothingModified)
            {
                await tx.RollbackAsync();
                return new RepositoryActionResult<TEntity>(entity, result.Status);
            }

            await tx.CommitAsync();

            return new RepositoryActionResult<TEntity>(result.Entity, result.Status);
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync();
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
        }
    }

    public virtual async Task<RepositoryActionResult<TEntity>> AddAsync(TEntity entity,
    Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, string>> sortOrder, string keySelector,
    int propertyLength, IDbContextTransaction tx)
    {
        try
        {
            var lastEntity = DbSet.Where(filter).OrderByDescending(sortOrder).ToArray().FirstOrDefault();

            var lastCode = string.Empty;


            if (lastEntity == null)
            {
                lastCode = "1";
            }
            else
            {
                var properties = lastEntity.GetType().GetProperties().ToList();
                foreach (var property in properties)
                {
                    if (property.Name != keySelector) continue;
                    var value = property.GetValue(lastEntity, null);
                    lastCode = (value?.ToString().ToNumValue() + 1)
                        .ToNumValue().ToString(CultureInfo.InvariantCulture);
                    break;
                }
            }

            var serial = GetEntityCode(lastCode, propertyLength);
            var targetProperties = entity.GetType().GetProperties().ToList();
            foreach (var property in targetProperties.Where(property => property.Name == keySelector))
            {
                property.SetValue(entity, serial, null);
                break;
            }

            DbSet.Add(entity);

            var result = await SaveChangesAsync();
            if (result == 0)
            {
                return new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.NothingModified);
            }

            return new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.Created);
        }
        catch (Exception ex)
        {
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
        }
    }

    private static string GetEntityCode(string lastCode, int length)
    {
        return length switch
        {
            2 => lastCode.ToTwoChar(),
            3 => lastCode.ToThreeChar(),
            4 => lastCode.ToFourChar(),
            5 => lastCode.ToFiveChar(),
            6 => lastCode.ToSixChar(),
            7 => lastCode.ToSevenChar(),
            _ => lastCode
        };
    }

    public virtual async Task<RepositoryActionResult<IEnumerable<TEntity>>> AddManyAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            DbSet.AddRange(enumerable);
            var result = await SaveChangesAsync();
            return result != 0
                ? new RepositoryActionResult<IEnumerable<TEntity>>(enumerable, RepositoryActionStatus.Created)
                : new RepositoryActionResult<IEnumerable<TEntity>>(enumerable,
                    RepositoryActionStatus.NothingModified);
        }
        catch (Exception ex)
        {
            return new RepositoryActionResult<IEnumerable<TEntity>>(null, RepositoryActionStatus.Error, ex);
        }
    }

    public virtual async Task<RepositoryActionResult<IEnumerable<TEntity>>> EditManyAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            var result = await SaveChangesAsync();
            return result != 0
                ? new RepositoryActionResult<IEnumerable<TEntity>>(enumerable, RepositoryActionStatus.Created)
                : new RepositoryActionResult<IEnumerable<TEntity>>(enumerable,
                    RepositoryActionStatus.NothingModified);
        }
        catch (Exception ex)
        {
            return new RepositoryActionResult<IEnumerable<TEntity>>(null, RepositoryActionStatus.Error, ex);
        }
    }

    public virtual async Task<RepositoryActionResult<TEntity>> DeleteAsync(TEntity entity)
    {
        try
        {
            var existingEntity = await ItemToGetAsync(entity);
            if (existingEntity == null)
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.NotFound);
            Context.Entry(existingEntity).State = EntityState.Deleted;
            var result = await SaveChangesAsync();
            return result > 0
                ? new RepositoryActionResult<TEntity>(entity, RepositoryActionStatus.Deleted)
                : new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.NotFound);
        }
        catch (Exception ex)
        {
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
        }
    }

    public virtual async Task<RepositoryActionResult<TEntity>> TruncateAsync()
    {
        try
        {
            var itemsToDelete = await DbSet.ToArrayAsync();
            DbSet.RemoveRange(itemsToDelete);
            var result = await SaveChangesAsync();
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Deleted);
        }
        catch (Exception ex)
        {
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
        }
    }

    public virtual async Task<RepositoryActionResult<IEnumerable<TEntity>>> LoadAsync(IEnumerable<TEntity> entities)
    {
        await using var tx = await Context.Database.BeginTransactionAsync();
        try
        {
            var truncate = await TruncateAsync();
            if (truncate.Status != RepositoryActionStatus.Deleted)
            {
                await tx.RollbackAsync();
                return new RepositoryActionResult<IEnumerable<TEntity>>(null, RepositoryActionStatus.Error);
            }

            var result = await AddManyAsync(entities);
            if (result.Status != RepositoryActionStatus.Created) await tx.RollbackAsync();
            return result;
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync();
            return new RepositoryActionResult<IEnumerable<TEntity>>(null, RepositoryActionStatus.Error, ex);
        }
    }

    public virtual async Task<RepositoryActionResult<TEntity>> DeleteManyAsync(Expression<Func<TEntity, bool>> where)
    {
        try
        {
            var entities = DbSet.Where(where).AsEnumerable();
            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }

            await SaveChangesAsync();
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Deleted);
        }
        catch (Exception ex)
        {
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
        }
    }

    public virtual async Task<RepositoryActionResult<TEntity>> EditAsync(TEntity entity)
    {
        try
        {
            var existingEntity = await ItemToGetAsync(entity);
            if (existingEntity == null)
            {
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.NotFound);
            }

            Context.Entry(existingEntity).State = EntityState.Detached;
            existingEntity = PropertyMapper.PropertyMapSelective(entity, existingEntity);
            DbSet.Attach(existingEntity);
            Context.Entry(existingEntity).State = EntityState.Modified;
            var result = await SaveChangesAsync();
            return result == 0
                ? new RepositoryActionResult<TEntity>(existingEntity, RepositoryActionStatus.NothingModified, null)
                : new RepositoryActionResult<TEntity>(existingEntity, RepositoryActionStatus.Updated);
        }
        catch (Exception ex)
        {
            return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
        }
    }

    public virtual async Task<TEntity[]> GetAllAsync() => await ItemsToGetAsync();

    public virtual async Task<TEntity[]> GetAllAsync(Expression<Func<TEntity, string>> sortKey) =>
        await DbSet.OrderBy(sortKey).AsNoTracking().ToArrayAsync();

    public virtual async Task<TEntity[]> GetAllAsync(int page, int count)
    {
        return await ItemsToGetAsync(page, count);
    }

    public virtual async Task<TEntity> GetAsync(TEntity entity) => await ItemToGetAsync(entity);

    public virtual async Task<TEntity> GetAsync(string tenant, string entity) => await ItemToGetAsync(tenant, entity);

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where) =>
        await DbSet.Where(where).FirstOrDefaultAsync();

    public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> where) =>
        await DbSet.AsNoTracking().FirstOrDefaultAsync(where);

    public virtual async Task<TEntity> GetFirstOrDefaultAsync() =>
        await DbSet.AsNoTracking().FirstOrDefaultAsync();

    public virtual async Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where) =>
        await DbSet.AsNoTracking().Where(where).ToArrayAsync();

    public virtual async Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, string>> orderBy) =>
        await DbSet.AsNoTracking().Where(where).OrderBy(orderBy).ToArrayAsync();

    public virtual async Task<TEntity[]> GetManyAsync(Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, string>> orderBy, Expression<Func<TEntity, string>> thenBy) =>
        await DbSet.AsNoTracking().Where(where).OrderBy(orderBy).ThenBy(thenBy).ToArrayAsync();

    protected virtual async Task<TEntity[]> ItemsToGetAsync() => await DbSet.AsNoTracking().ToArrayAsync();

    protected virtual async Task<TEntity[]> ItemsToGetAsync(int page, int count)
    {
        if (page < 1) page = 1;
        return await DbSet.AsNoTracking().Skip(page * count).Take(count).ToArrayAsync();
    }

    protected virtual async Task<TEntity> ItemToGetAsync(TEntity entity) => await Task.FromResult(entity);

    protected virtual async Task<TEntity> ItemToGetAsync(string tenant, string entity) => await Task.FromResult(new TEntity());

    protected async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();

    public async Task<int> GetCountAsync() => await DbSet.CountAsync();

    protected override void DisposeCore()
    {
        Context?.Dispose();
        Context = null;
    }
}