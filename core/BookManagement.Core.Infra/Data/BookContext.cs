using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookManagement.Core.Infra.Data.Extensions;
using BookManagement.Core.Infra.Data.Interfaces;
using BookManagement.Core.Shared.Entities.Interfaces;
using BookManagement.Core.Shared.Extensions.IQueryableExtensions;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Core.Shared.Queries.QueryResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;

namespace BookManagement.Core.Infra.Data;

public class BookContext : IBookContext
{
    private readonly ApplicationContext _applicationContext;
    private readonly IMapper _mapper;
    private IDbContextTransaction _transaction;

    public BookContext(ApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<bool> CreateAsync<TEntity>(TEntity entity) where TEntity : IBaseEntity
    {
        async Task<bool> Insert()
        {
            _applicationContext.Add(entity);

            var result = await _applicationContext.SaveChangesAsync();
            return result > 0;
        }

        return await this.ExecuteStrategyAndRetry(_applicationContext, Insert);
    }

    public async Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : IBaseEntity
    {
        async Task<bool> Update()
        {
            _applicationContext.Update(entity);

            var result = await _applicationContext.SaveChangesAsync();
            return result > 0;
        }

        return await this.ExecuteStrategyAndRetry(_applicationContext, Update);
    }

    public async Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : class, IBaseEntity
    {
        async Task<TEntity> GetById()
        {
            var entity = await _applicationContext
                .GetDbSet<TEntity>()
                .FindAsync(id);

            return entity;
        }

        return await this.ExecuteRetry(GetById);
    }

    public async Task<TEntity> GetByIdAsync<TEntity>(Guid id, params Expression<Func<TEntity, object>>[] includes)
        where TEntity : class, IBaseEntity
    {
        async Task<TEntity> GetById()
        {
            var query = _applicationContext
                .GetDbSet<TEntity>()
                .Where(x => x.Id == id);

            query = includes.Aggregate(query, (current, expressionProperty) => current.Include(expressionProperty));

            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }

        return await this.ExecuteRetry(GetById);
    }

    public async Task<TEntity> FirstOrDefaultAsync<TEntity>(bool readOnly, Expression<Func<TEntity, bool>> where)
        where TEntity : class
    {
        async Task<TEntity> GetFirstOrDefault()
        {
            var entity = await _applicationContext.GetDbSet<TEntity>()
                .AsNoTracking(readOnly)
                .FirstOrDefaultAsync(where);

            return entity;
        }

        return await this.ExecuteRetry(GetFirstOrDefault);
    }

    public async Task<List<TEntity>> WhereAsync<TEntity>(bool readOnly, Expression<Func<TEntity, bool>> where,
        params Expression<Func<TEntity, object>>[] includes)
        where TEntity : class
    {
        async Task<List<TEntity>> GetList()
        {
            var queryable = _applicationContext
                .GetDbSet<TEntity>()
                .AsNoTracking(readOnly)
                .Where(where);

            if (includes?.Length > 0)
                queryable = includes.Aggregate(queryable,
                    (current, expressionProperty) => current.Include(expressionProperty));

            return await queryable.ToListAsync();
        }

        return await this.ExecuteRetry(GetList);
    }

    public async Task<IPaginateQueryResult<TQueryResult>> GetPaginatedAsync<TQueryResult, TEntity>(int skip,
        int pageSize, string orderyBy, IQueryable<TEntity> queryable)
        where TEntity : class
    {
        async Task<IPaginateQueryResult<TQueryResult>> GetPaginated()
        {
            var countToSkip = (skip - 1) * pageSize;

            var query = queryable.OrderBy(orderyBy)
                .Skip(countToSkip)
                .Take(pageSize);

            var countRows = await queryable.CountAsync();
            var countPages = countRows / pageSize;

            var entitiesPaginated = await query.ToListAsync();
            var queryResult = _mapper.Map<List<TQueryResult>>(entitiesPaginated);

            return new PaginateQueryResult<TQueryResult>(skip, countPages, pageSize, countRows, orderyBy, queryResult);
        }

        return await this.ExecuteRetry(GetPaginated);
    }

    public async Task<IPaginateQueryResult<TQueryResult>> GetPaginatedProjectionAsync<TQueryResult, TEntity>(
        int skip, int pageSize, string orderBy, IQueryable<TEntity> queryable)
        where TEntity : class
    {
        async Task<IPaginateQueryResult<TQueryResult>> GetPaginated()
        {
            var countToSkip = (skip - 1) * pageSize;

            var query = queryable.OrderBy(orderBy)
                .Skip(countToSkip)
                .Take(pageSize);

            var countRows = await queryable.CountAsync();
            var countPages = countRows / pageSize;

            var queryResult = await query
                .ProjectTo<TQueryResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginateQueryResult<TQueryResult>(skip, countPages, pageSize, countRows, orderBy, queryResult);
        }

        return await this.ExecuteRetry(GetPaginated);
    }

    public async Task<List<TEntity>> AllAsync<TEntity>() where TEntity : class
    {
        async Task<List<TEntity>> GetAll()
        {
            var entities = await _applicationContext
                .GetDbSet<TEntity>()
                .AsNoTracking()
                .ToListAsync();

            return entities;
        }

        return await this.ExecuteRetry(GetAll);
    }

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
    {
        async Task<bool> GetAny()
        {
            var any = await _applicationContext
                .GetDbSet<TEntity>()
                .AsNoTracking()
                .AnyAsync(where);

            return any;
        }

        return await this.ExecuteRetry(GetAny);
    }

    public IQueryable<TEntity> GetQueryable<TEntity>(bool readOnly,
        params Expression<Func<TEntity, object>>[] typesToInclude) where TEntity : class
    {
        var queryable = _applicationContext.GetDbSet<TEntity>()
            .AsNoTracking(readOnly)
            .AsQueryable();

        return typesToInclude.Aggregate(queryable,
            (current, expressionProperty) => current.Include(expressionProperty));
    }

    public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable)
    {
        await this.ExecuteStrategyAndRetry(_applicationContext,
            async () => { _transaction = await _applicationContext.Database.BeginTransactionAsync(isolationLevel); });
    }

    public async Task CommitAsync()
    {
        await this.ExecuteStrategyAndRetry(_applicationContext, () => _transaction?.CommitAsync());
    }

    public async Task RollbackAsync()
    {
        await this.ExecuteStrategyAndRetry(_applicationContext, () => _transaction?.RollbackAsync());
    }
}