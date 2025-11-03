using MyApp.Comments.Core.Domain;
using System.Linq.Expressions;

namespace MyApp.Comments.Core.Application;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> All();
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<T?> GetAsync(
    Expression<Func<T, bool>> predicate,
    Func<IQueryable<T>, IQueryable<T>>? include = null,
    CancellationToken cancellationToken = default);
    Task<List<T>> ListAsync(CancellationToken cancellationToken);
    Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<List<TResult>> ListAsync<TResult>(
    Expression<Func<T, bool>>? predicate = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    Expression<Func<T, TResult>>? selector = null,
    int? skip = null,
    int? take = null,
    CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}
