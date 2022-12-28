using BookManagement.Core.Shared.Entities.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using System.Data;
using System.Linq.Expressions;

namespace BookManagement.Core.Infra.Data.Interfaces;

public interface IBookContext
{
    Task<bool> CreateAsync<TEntity>(TEntity entity) where TEntity : IBaseEntity;
    Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : IBaseEntity;
    Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : class, IBaseEntity;
    Task<TEntity> GetByIdAsync<TEntity>(Guid id, params Expression<Func<TEntity, object>>[] includes) where TEntity : class, IBaseEntity;
    Task<TEntity> FirstOrDefaultAsync<TEntity>(bool readOnly, Expression<Func<TEntity, bool>> where) where TEntity : class;
    Task<List<TEntity>> WhereAsync<TEntity>(bool readOnly, Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes) where TEntity : class;
    Task<IPaginateQueryResult<TQueryResult>> GetPaginatedAsync<TQueryResult, TEntity>(int skip, int pageSize, string orderyBy, IQueryable<TEntity> queryable) where TEntity : class;
    Task<IPaginateQueryResult<TQueryResult>> GetPaginatedProjectionAsync<TQueryResult, TEntity>(int skip, int pageSize, string orderyBy, IQueryable<TEntity> queryable) where TEntity : class;
    Task<List<TEntity>> AllAsync<TEntity>() where TEntity : class;
    Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
    IQueryable<TEntity> GetQueryable<TEntity>(bool readOnly, params Expression<Func<TEntity, object>>[] typesToInclude) where TEntity : class;

    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable);
    Task CommitAsync();
    Task RollbackAsync();
}