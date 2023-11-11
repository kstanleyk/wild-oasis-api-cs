using Core.Common.Contracts;
using Core.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace WildOasis.Infrastructure.Persistence;

public abstract class DataPersistenceBase<TEntity> :  DataPersistenceBase<TEntity, WildOasisContext>
    where TEntity : class, IIdentifiableEntity, new()
{
    protected IDatabaseFactory DatabaseFactory;
    protected string ConnectionString;

    protected DataPersistenceBase(IDatabaseFactory databaseFactory) : base(databaseFactory.GetContext())
    {
        DatabaseFactory = databaseFactory;
        Db = databaseFactory.GetConnection();
        ConnectionString = DatabaseFactory.GetContext().Database.GetDbConnection().ConnectionString;
    }

    protected override void DisposeCore()
    {
        DatabaseFactory?.Dispose();
        base.DisposeCore();
    }
}