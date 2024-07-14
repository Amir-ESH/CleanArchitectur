using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Domain.Commons;
using System.Collections.Frozen;
using System.Linq.Expressions;

// ReSharper disable MethodOverloadWithOptionalParameter

namespace CleanArchitecture.Application.Common.Interfaces;

public interface ICacheRepository<TCacheEntity, TEntityKey>
    where TCacheEntity : CacheBaseEntity<TEntityKey>, ICacheEntity
{
    DbSet<TCacheEntity> Entities { get; }
    IQueryable<TCacheEntity> Table { get; }
    ValueTask<TCacheEntity?> GetByIdAsync(CancellationToken cancellationToken, params TEntityKey[] ids);
    Task<IEnumerable<TCacheEntity>> AddRangeAsync(IEnumerable<TCacheEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    Task<bool> UpdateRangeAsync(IEnumerable<TCacheEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    Task ExecuteSqlQueryWithoutResultAsync(CancellationToken cancellationToken, string query);
    Task<IReadOnlyList<TCacheEntity>?> GetAllAsync(CancellationToken cancellationToken);
    Task<ResultDto<IReadOnlyList<TCacheEntity>?>> GetAllAsync(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<TCacheEntity>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>> predicate);
    Task<ResultDto<IReadOnlyList<TCacheEntity>?>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>> predicate,
                                                   int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<TCacheEntity>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>>? predicate = null,
                                        Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
                                        string? includeString = null,
                                        bool disableTracking = true);
    Task<ResultDto<IReadOnlyList<TCacheEntity>?>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>>? predicate = null,
                                                   Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
                                                   string? includeString = null,
                                                   bool disableTracking = true, int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<TCacheEntity>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>>? predicate = null,
                                        Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
                                        List<Expression<Func<TCacheEntity, object>>>? includes = null,
                                        bool disableTracking = true);

    Task<ResultDto<IReadOnlyList<TCacheEntity>?>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>>? predicate = null,
                                                  Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
                                                  List<Expression<Func<TCacheEntity, object>>>? includes = null,
                                                  bool disableTracking = true, int pageNumber = 1, int pageSize = 10);
    Task<TCacheEntity?> GetAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>> predicate);
    Task<TCacheEntity?> GetAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>>? predicate = null,
                      Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
                      string? includeString = null,
                      bool disableTracking = true);
    Task<TCacheEntity?> GetAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>>? predicate = null,
                      Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
                      List<Expression<Func<TCacheEntity, object>>>? includes = null,
                      bool disableTracking = true);
    Task<TCacheEntity?> GetByIdAsync(CancellationToken cancellationToken, TEntityKey id);
    Task<TCacheEntity?> GetByIdAsync(TEntityKey id);
    Task<TCacheEntity> AddAsync(CancellationToken cancellationToken, TCacheEntity entity);
    Task<TEntityKey> AddAndGetIdAsync(CancellationToken cancellationToken, TCacheEntity entity);
    Task<bool> UpdateAsync(CancellationToken cancellationToken, TCacheEntity entity);
    Task<bool> UpdateRangeAsync(CancellationToken cancellationToken, List<TCacheEntity> entities);
    Task<bool> DeleteAsync(CancellationToken cancellationToken, TCacheEntity entity);
    Task<bool> DeleteAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>> predicate);
    Task<bool> GetAnyAsync(CancellationToken cancellationToken, Expression<Func<TCacheEntity, bool>> predicate);
    FrozenSet<TCacheEntity>? GetAll(int? pageNumber = null, int? pageSize = null);
    FrozenSet<TCacheEntity>? GetAll(Expression<Func<TCacheEntity, bool>> predicate, int? pageNumber = null, int? pageSize = null);
    FrozenSet<TCacheEntity>? GetAll(Expression<Func<TCacheEntity, bool>>? predicate = null,
                         Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
                         string? includeString = null,
                         bool disableTracking = true, int? pageNumber = null, int? pageSize = null);
    FrozenSet<TCacheEntity>? GetAll(Expression<Func<TCacheEntity, bool>>? predicate = null,
                         Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
                         List<Expression<Func<TCacheEntity, object>>>? includes = null,
                         bool disableTracking = true, int? pageNumber = null, int? pageSize = null);
    TCacheEntity? Get(Expression<Func<TCacheEntity, bool>> predicate);
    TCacheEntity? Get(Expression<Func<TCacheEntity, bool>>? predicate = null,
           Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
           string? includeString = null,
           bool disableTracking = true);
    TCacheEntity? Get(Expression<Func<TCacheEntity, bool>>? predicate = null,
           Func<IQueryable<TCacheEntity>, IOrderedQueryable<TCacheEntity>>? orderBy = null,
           List<Expression<Func<TCacheEntity, object>>>? includes = null,
           bool disableTracking = true);
    TCacheEntity? GetById(TEntityKey id);
    TCacheEntity Add(TCacheEntity entity);
    TEntityKey AddAndGetId(TCacheEntity entity);
    bool Update(TCacheEntity entity);
    bool UpdateRange(List<TCacheEntity> entities);
    bool Delete(TCacheEntity entity);
    bool Delete(Expression<Func<TCacheEntity, bool>> predicate);
    bool GetAny(Expression<Func<TCacheEntity, bool>> predicate);
    Task LoadCollectionAsync<TProperty>(TCacheEntity entity,
                                        Expression<Func<TCacheEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class;
    void LoadCollection<TProperty>(TCacheEntity entity, Expression<Func<TCacheEntity, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class;
    Task LoadReferenceAsync<TProperty>(TCacheEntity entity, Expression<Func<TCacheEntity, TProperty>> referenceProperty,
                                       CancellationToken cancellationToken)
        where TProperty : class;
    void LoadReference<TProperty>(TCacheEntity entity, Expression<Func<TCacheEntity, TProperty>> referenceProperty)
        where TProperty : class;
}
