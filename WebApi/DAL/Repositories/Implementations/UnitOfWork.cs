
using DAL.Data.DbContext;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Domains;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShipingContext _ctx;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();
        private IDbContextTransaction? _tx;
        private readonly ILoggerFactory _loggerFactory;

        public UnitOfWork(ShipingContext ctx, ILoggerFactory loggerFactory)
        {
            _ctx = ctx;
            _loggerFactory = loggerFactory;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            return (IGenericRepository<T>)_repositories.GetOrAdd(
                typeof(T),
                _ => new GenericRepository<T>(
                        _ctx,
                        _loggerFactory.CreateLogger<GenericRepository<T>>()));
        }

        public async Task BeginTransactionAsync()  => _tx = await _ctx.Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            await _ctx.SaveChangesAsync();
            if (_tx is not null) await _tx.CommitAsync();
        }

        public async Task RollbackAsync()
            => await _tx?.RollbackAsync()!;

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        {
            if (_tx is not null) await _tx.DisposeAsync();
            await _ctx.DisposeAsync();
        }
    }

}
