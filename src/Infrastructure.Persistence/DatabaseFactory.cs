using System;
using System.Data;
using Core.Common.Core;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace WildOasis.Infrastructure.Persistence;

public class DatabaseFactory : Disposable, IDatabaseFactory
{
    public DatabaseFactory(WildOasisContext dataContext)
    {
        _dataContext = dataContext;
        _db = new NpgsqlConnection(GetContext().Database.GetDbConnection().ConnectionString);
    }

    private WildOasisContext _dataContext;
    private readonly IDbConnection _db;

    public WildOasisContext GetContext()
    {
        return _dataContext;
    }

    public IDbConnection GetConnection()
    {
        return _db;
    }

    protected override void DisposeCore()
    {
        _dataContext?.Dispose();
        _dataContext = null;
    }
}

public interface IDatabaseFactory : IDisposable
{
    WildOasisContext GetContext();
    IDbConnection GetConnection();
}