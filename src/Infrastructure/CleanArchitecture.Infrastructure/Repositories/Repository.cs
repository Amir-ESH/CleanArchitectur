using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Frozen;
using System.Linq.Expressions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Data;

// ReSharper disable MethodOverloadWithOptionalParameter

namespace CleanArchitecture.Infrastructure.Repositories;

public class Repository<TEntity, TEntityKey> : IRepository<TEntity, TEntityKey>
    where TEntity : BaseEntity<TEntityKey>, IEntity, new()
{
    protected readonly DataContext DbContext;
    private readonly DbSet<TEntity> _query;
    public DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;

    public Repository(DataContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>();
        _query = DbContext.Set<TEntity>();
    }

    #region Repositories

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <param name="query">SQL Query Without Result</param>
    /// <returns>Without Result</returns>
    public async Task ExecuteSqlQueryWithoutResultAsync(CancellationToken cancellationToken, string query)
    {
        try
        {
            await DbContext.Database.ExecuteSqlRawAsync(query, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _query.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    public async Task<ResultDto<IReadOnlyList<TEntity>?>> GetAllAsync(CancellationToken cancellationToken, int pageNumber = 1,
                                                                int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken,
                                                             Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return await _query.Where(predicate)
                               .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    public async Task<ResultDto<IReadOnlyList<TEntity>?>> GetAllAsync(CancellationToken cancellationToken,
                                                                Expression<Func<TEntity, bool>> predicate, int pageNumber = 1,
                                                                int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeString"></param>
    /// <param name="disableTracking"></param>
    /// <returns></returns>
    public virtual async Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken,
                                                             Expression<Func<TEntity, bool>>? predicate,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
                                                             string? includeString,
                                                             bool disableTracking = true)
    {
        try
        {
            var query = _query.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            return orderBy is null
                       ? await query.ToListAsync(cancellationToken)
                       : await orderBy(query).ToListAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    public async Task<ResultDto<IReadOnlyList<TEntity>?>> GetAllAsync(CancellationToken cancellationToken,
                                                                Expression<Func<TEntity, bool>>? predicate = null,
                                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy =
                                                                    null,
                                                                string? includeString = null,
                                                                bool disableTracking = true, int pageNumber = 1,
                                                                int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <param name="disableTracking"></param>
    /// <returns></returns>
    public virtual async Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken,
                                                             Expression<Func<TEntity, bool>>? predicate,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
                                                             List<Expression<Func<TEntity, object>>>? includes,
                                                             bool disableTracking = true)
    {
        try
        {
            var query = _query.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            return orderBy is null
                       ? await query.ToListAsync(cancellationToken)
                       : await orderBy(query).ToListAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    public async Task<ResultDto<IReadOnlyList<TEntity>?>> GetAllAsync(CancellationToken cancellationToken,
                                                                Expression<Func<TEntity, bool>>? predicate = null,
                                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy =
                                                                    null,
                                                                List<Expression<Func<TEntity, object>>>? includes = null,
                                                                bool disableTracking = true, int pageNumber = 1,
                                                                int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<TEntity?> GetAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return await _query.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeString"></param>
    /// <param name="disableTracking"></param>
    /// <returns></returns>
    public virtual async Task<TEntity?> GetAsync(CancellationToken cancellationToken,
                                           Expression<Func<TEntity, bool>>? predicate = null,
                                           // ReSharper disable once MethodOverloadWithOptionalParameter
                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                           string? includeString = null,
                                           bool disableTracking = true)
    {
        try
        {
            var query = _query.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            return orderBy is null
                       ? await query.FirstOrDefaultAsync(cancellationToken)
                       : await orderBy(query).FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <param name="disableTracking"></param>
    /// <returns></returns>
    public virtual async Task<TEntity?> GetAsync(CancellationToken cancellationToken,
                                           Expression<Func<TEntity, bool>>? predicate = null,
                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                           List<Expression<Func<TEntity, object>>>? includes = null,
                                           bool disableTracking = true)
    {
        try
        {
            var query = _query.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            return orderBy is null
                       ? await query.FirstOrDefaultAsync(cancellationToken)
                       : await orderBy(query).FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<TEntity?> GetByIdAsync(CancellationToken cancellationToken, TEntityKey id)
    {
        return await _query.FindAsync(id, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TEntity?> GetByIdAsync(TEntityKey id)
    {
        try
        {
            return await _query.FindAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<TEntityKey> AddAndGetIdAsync(CancellationToken cancellationToken, TEntity entity)
    {
        try
        {
            await _query.AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> AddAsync(CancellationToken cancellationToken, TEntity entity)
    {
        try
        {
            await _query.AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<bool> UpdateAsync(CancellationToken cancellationToken, TEntity entity)
    {
        try
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    public async Task<bool> UpdateRangeAsync(CancellationToken cancellationToken, List<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteByIdAsync(CancellationToken cancellationToken, TEntityKey id)
    {
        try
        {
            //var entity = await GetByIdAsync(id);

            var entity = new TEntity {Id = id};


            //if (entity == null) return false;

            _query.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    public async Task<bool> SoftDeleteAsync(CancellationToken cancellationToken, TEntity entity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<bool> GetAnyAsync(CancellationToken cancellationToken,
                                                Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return await _query.AnyAsync(predicate, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, TEntity entity)
    {
        try
        {
            _query.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken,
                                                Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            if (await GetAllAsync(cancellationToken, predicate) is { } data)
            {
                _query.RemoveRange(data);
                await DbContext.SaveChangesAsync(cancellationToken);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    public async Task<bool> SoftDeleteAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual FrozenSet<TEntity>? GetAll(int? pageNumber = null, int? pageSize = null)
    {
        try
        {
            if (pageNumber == null || pageSize == null)
                return _query.ToFrozenSet();

            return _query.Skip(((int) pageNumber - 1) * (int) pageSize).Take((int) pageSize).ToFrozenSet();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public virtual FrozenSet<TEntity>? GetAll(Expression<Func<TEntity, bool>> predicate, int? pageNumber = null,
                                        int? pageSize = null)
    {
        try
        {
            if (pageNumber == null || pageSize == null)
                return _query.Where(predicate).ToFrozenSet();

            return _query.Where(predicate).Skip(((int) pageNumber - 1) * (int) pageSize).Take((int) pageSize)
                         .ToFrozenSet();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeString"></param>
    /// <param name="disableTracking"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public virtual FrozenSet<TEntity>? GetAll(Expression<Func<TEntity, bool>>? predicate,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                        string? includeString = null,
                                        bool disableTracking = true, int? pageNumber = null, int? pageSize = null)
    {
        try
        {
            var query = _query.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (pageNumber == null || pageSize == null)
            {
                return orderBy != null
                           ? orderBy(query).ToFrozenSet()
                           : query.ToFrozenSet();
            }

            return orderBy != null
                       ? orderBy(query).Skip(((int) pageNumber - 1) * (int) pageSize).Take((int) pageSize)
                                       .ToFrozenSet()
                       : query.Skip(((int) pageNumber - 1) * (int) pageSize).Take((int) pageSize).ToFrozenSet();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <param name="disableTracking"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public virtual FrozenSet<TEntity>? GetAll(Expression<Func<TEntity, bool>>? predicate,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
                                        List<Expression<Func<TEntity, object>>>? includes, bool disableTracking = true,
                                        int? pageNumber = null, int? pageSize = null)
    {
        try
        {
            var query = _query.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (pageNumber == null || pageSize == null)
            {
                return orderBy != null
                           ? orderBy(query).ToFrozenSet()
                           : query.ToFrozenSet();
            }

            return orderBy != null
                       ? orderBy(query).Skip(((int) pageNumber - 1) * (int) pageSize).Take((int) pageSize)
                                       .ToFrozenSet()
                       : query.Skip(((int) pageNumber - 1) * (int) pageSize).Take((int) pageSize).ToFrozenSet();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual TEntity? Get(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return _query.FirstOrDefault(predicate);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeString"></param>
    /// <param name="disableTracking"></param>
    /// <returns></returns>
    public virtual TEntity? Get(Expression<Func<TEntity, bool>>? predicate = null,
                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string? includeString = null,
                          bool disableTracking = true)
    {
        try
        {
            var query = _query.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <param name="disableTracking"></param>
    /// <returns></returns>
    public virtual TEntity? Get(Expression<Func<TEntity, bool>>? predicate = null,
                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                          List<Expression<Func<TEntity, object>>>? includes = null, bool disableTracking = true)
    {
        try
        {
            var query = _query.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual TEntity? GetById(TEntityKey id)
    {
        try
        {
            return _query.Find(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual TEntity Add(TEntity entity)
    {
        try
        {
            _query.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual TEntityKey AddAndGetId(TEntity entity)
    {
        try
        {
            _query.Add(entity);
            DbContext.SaveChanges();
            return entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual bool Update(TEntity entity)
    {
        try
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    public bool UpdateRange(List<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual bool DeleteById(TEntityKey id)
    {
        try
        {
            //var entity = await GetByIdAsync(id);

            var entity = new TEntity {Id = id};

            //if (entity == null) return false;

            _query.Remove(entity);
            DbContext.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    public bool SoftDelete(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual bool GetAny(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return _query.Any(predicate);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual bool Delete(TEntity entity)
    {
        try
        {
            _query.Remove(entity);
            DbContext.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    public virtual bool Delete(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            _query.RemoveRange(_query.Where(predicate));
            DbContext.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }

    public bool SoftDelete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Async Method

    public virtual ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params TEntityKey[] ids)
    {
        return Entities.FindAsync(ids, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities,
                                                            CancellationToken cancellationToken,
                                                            bool saveNow = true)
    {
        try
        {
            var addRangeAsync = entities.ToList();
            Assert.NotNull(addRangeAsync, nameof(entities));
            await Entities.AddRangeAsync(addRangeAsync, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return addRangeAsync;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw;
        }
    }

    public virtual async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
                                                     bool saveNow = true)
    {
        try
        {
            var baseEntities = entities.ToList();
            Assert.NotNull(baseEntities, nameof(entities));
            Entities.UpdateRange(baseEntities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return false;
        }
    }


    #endregion

    #region Attach & Detach

    public virtual void Detach(CancellationToken cancellationToken, TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        var entry = DbContext.Entry(entity);
        entry.State = EntityState.Detached;
    }

    public virtual void Attach(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        if (DbContext.Entry(entity).State == EntityState.Detached)
            Entities.Attach(entity);
    }

    #endregion

    #region Explicit Loading

    public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity,
                                                             Expression<Func<TEntity, IEnumerable<TProperty>>>
                                                                 collectionProperty,
                                                             CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);

        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
            await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual void LoadCollection<TProperty>(TEntity entity,
                                                  Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class
    {
        Attach(entity);
        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
            collection.Load();
    }

    public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity,
                                                            Expression<Func<TEntity, TProperty>> referenceProperty,
                                                            CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);
        var reference = DbContext.Entry(entity).Reference(referenceProperty!);
        if (!reference.IsLoaded)
            await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
        where TProperty : class
    {
        Attach(entity);
        var reference = DbContext.Entry(entity).Reference(referenceProperty!);
        if (!reference.IsLoaded)
            reference.Load();
    }

    #endregion
}
