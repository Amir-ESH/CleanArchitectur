using CleanArchitecture.Application.Common.DTO;
using System.Collections.Frozen;
using System.Linq.Expressions;
using CleanArchitecture.Domain.Commons;

// ReSharper disable MethodOverloadWithOptionalParameter

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IRepository<T, TEntityKey> where T : BaseEntity<TEntityKey>, IEntity
{
    DbSet<T> Entities { get; }
    IQueryable<T> Table { get; }
    IQueryable<T> TableNoTracking { get; }
    ValueTask<T?> GetByIdAsync(CancellationToken cancellationToken, params TEntityKey[] ids);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true);
    Task<bool> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true);
    Task ExecuteSqlQueryWithoutResultAsync(CancellationToken cancellationToken, string query);
    Task<IReadOnlyList<T>?> GetAllAsync(CancellationToken cancellationToken);
    Task<ResultDto<IReadOnlyList<T>?>> GetAllAsync(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<T>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate);
    Task<ResultDto<IReadOnlyList<T>?>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate,
                                                   int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<T>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>>? predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                        string? includeString = null,
                                        bool disableTracking = true);
    Task<ResultDto<IReadOnlyList<T>?>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>>? predicate = null,
                                                   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                   string? includeString = null,
                                                   bool disableTracking = true, int pageNumber = 1, int pageSize = 10);
    Task<IReadOnlyList<T>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>>? predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                        List<Expression<Func<T, object>>>? includes = null,
                                        bool disableTracking = true);

    Task<ResultDto<IReadOnlyList<T>?>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>>? predicate = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                  List<Expression<Func<T, object>>>? includes = null,
                                                  bool disableTracking = true, int pageNumber = 1, int pageSize = 10);
    Task<T?> GetAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate);
    Task<T?> GetAsync(CancellationToken cancellationToken, Expression<Func<T, bool>>? predicate = null,
                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                      string? includeString = null,
                      bool disableTracking = true);
    Task<T?> GetAsync(CancellationToken cancellationToken, Expression<Func<T, bool>>? predicate = null,
                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                      List<Expression<Func<T, object>>>? includes = null,
                      bool disableTracking = true);
    Task<T?> GetByIdAsync(CancellationToken cancellationToken, TEntityKey id);
    Task<T?> GetByIdAsync(TEntityKey id);
    Task<T> AddAsync(CancellationToken cancellationToken, T entity);
    Task<TEntityKey> AddAndGetIdAsync(CancellationToken cancellationToken, T entity);
    Task<bool> UpdateAsync(CancellationToken cancellationToken, T entity);
    Task<bool> UpdateRangeAsync(CancellationToken cancellationToken, List<T> entities);
    Task<bool> DeleteAsync(CancellationToken cancellationToken, T entity);
    Task<bool> DeleteAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate);
    Task<bool> GetAnyAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate);
    FrozenSet<T>? GetAll(int? pageNumber = null, int? pageSize = null);
    FrozenSet<T>? GetAll(Expression<Func<T, bool>> predicate, int? pageNumber = null, int? pageSize = null);
    FrozenSet<T>? GetAll(Expression<Func<T, bool>>? predicate = null,
                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                         string? includeString = null,
                         bool disableTracking = true, int? pageNumber = null, int? pageSize = null);
    FrozenSet<T>? GetAll(Expression<Func<T, bool>>? predicate = null,
                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                         List<Expression<Func<T, object>>>? includes = null,
                         bool disableTracking = true, int? pageNumber = null, int? pageSize = null);
    T? Get(Expression<Func<T, bool>> predicate);
    T? Get(Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           string? includeString = null,
           bool disableTracking = true);
    T? Get(Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           List<Expression<Func<T, object>>>? includes = null,
           bool disableTracking = true);
    T? GetById(TEntityKey id);
    T Add(T entity);
    TEntityKey AddAndGetId(T entity);
    bool Update(T entity);
    bool UpdateRange(List<T> entities);
    bool Delete(T entity);
    bool Delete(Expression<Func<T, bool>> predicate);
    bool GetAny(Expression<Func<T, bool>> predicate);
    Task LoadCollectionAsync<TProperty>(T entity,
                                        Expression<Func<T, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class;
    void LoadCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class;
    Task LoadReferenceAsync<TProperty>(T entity, Expression<Func<T, TProperty>> referenceProperty,
                                       CancellationToken cancellationToken)
        where TProperty : class;
    void LoadReference<TProperty>(T entity, Expression<Func<T, TProperty>> referenceProperty)
        where TProperty : class;
}
