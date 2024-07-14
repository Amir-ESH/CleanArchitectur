using CleanArchitecture.Application.Common.DTO;
using System.Collections.Frozen;
using System.Linq.Expressions;
using CleanArchitecture.Domain.Commons;

// ReSharper disable MethodOverloadWithOptionalParameter

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IRepository<TEntity, TEntityKey>
    where TEntity : BaseEntity<TEntityKey>, IEntity
{
    DbSet<TEntity> Entities { get; }
    IQueryable<TEntity> Table { get; }
    ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params TEntityKey[] ids);
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    Task ExecuteSqlQueryWithoutResultAsync(CancellationToken cancellationToken, string query);
    Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken);
    Task<ResultDto<IReadOnlyList<TEntity>?>> GetAllAsync(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate);
    Task<ResultDto<IReadOnlyList<TEntity>?>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate,
                                                   int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                        string? includeString = null,
                                        bool disableTracking = true);
    Task<ResultDto<IReadOnlyList<TEntity>?>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null,
                                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                   string? includeString = null,
                                                   bool disableTracking = true, int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                        List<Expression<Func<TEntity, object>>>? includes = null,
                                        bool disableTracking = true);

    Task<ResultDto<IReadOnlyList<TEntity>?>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                  List<Expression<Func<TEntity, object>>>? includes = null,
                                                  bool disableTracking = true, int pageNumber = 1, int pageSize = 10);
    Task<TEntity?> GetAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null,
                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                      string? includeString = null,
                      bool disableTracking = true);
    Task<TEntity?> GetAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null,
                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                      List<Expression<Func<TEntity, object>>>? includes = null,
                      bool disableTracking = true);
    Task<TEntity?> GetByIdAsync(CancellationToken cancellationToken, TEntityKey id);
    Task<TEntity?> GetByIdAsync(TEntityKey id);
    Task<TEntity> AddAsync(CancellationToken cancellationToken, TEntity entity);
    Task<TEntityKey> AddAndGetIdAsync(CancellationToken cancellationToken, TEntity entity);
    Task<bool> UpdateAsync(CancellationToken cancellationToken, TEntity entity);
    Task<bool> UpdateRangeAsync(CancellationToken cancellationToken, List<TEntity> entities);
    Task<bool> DeleteAsync(CancellationToken cancellationToken, TEntity entity);
    Task<bool> DeleteAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate);
    Task<bool> GetAnyAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate);
    FrozenSet<TEntity>? GetAll(int? pageNumber = null, int? pageSize = null);
    FrozenSet<TEntity>? GetAll(Expression<Func<TEntity, bool>> predicate, int? pageNumber = null, int? pageSize = null);
    FrozenSet<TEntity>? GetAll(Expression<Func<TEntity, bool>>? predicate = null,
                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                         string? includeString = null,
                         bool disableTracking = true, int? pageNumber = null, int? pageSize = null);
    FrozenSet<TEntity>? GetAll(Expression<Func<TEntity, bool>>? predicate = null,
                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                         List<Expression<Func<TEntity, object>>>? includes = null,
                         bool disableTracking = true, int? pageNumber = null, int? pageSize = null);
    TEntity? Get(Expression<Func<TEntity, bool>> predicate);
    TEntity? Get(Expression<Func<TEntity, bool>>? predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           string? includeString = null,
           bool disableTracking = true);
    TEntity? Get(Expression<Func<TEntity, bool>>? predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           List<Expression<Func<TEntity, object>>>? includes = null,
           bool disableTracking = true);
    TEntity? GetById(TEntityKey id);
    TEntity Add(TEntity entity);
    TEntityKey AddAndGetId(TEntity entity);
    bool Update(TEntity entity);
    bool UpdateRange(List<TEntity> entities);
    bool Delete(TEntity entity);
    bool Delete(Expression<Func<TEntity, bool>> predicate);
    bool GetAny(Expression<Func<TEntity, bool>> predicate);
    Task LoadCollectionAsync<TProperty>(TEntity entity,
                                        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class;
    void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class;
    Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty,
                                       CancellationToken cancellationToken)
        where TProperty : class;
    void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
        where TProperty : class;
}
