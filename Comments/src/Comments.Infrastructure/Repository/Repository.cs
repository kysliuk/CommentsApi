using Comments.Application;
using Comments.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Comments.Infrastructure;

public class Repository<T>(DbContext context) : IRepository<T> where T : EntityBase
{
    public IQueryable<T> All()
    {
        return context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        context.Set<T>().Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await context.Set<T>().FindAsync([id], cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID '{id}' was not found.");

        return entity;
    }

    public async Task<T?> GetAsync(
    Expression<Func<T, bool>> predicate,
    Func<IQueryable<T>, IQueryable<T>>? include = null,
    CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = context.Set<T>();

        if (include != null)
            query = include(query);

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<List<T>> ListAsync(CancellationToken cancellationToken)
    {
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        context.Set<T>().Update(entity);
        await Task.CompletedTask;
    }

    public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await context.Set<T>()
               .Where(predicate)
               .ToListAsync(cancellationToken);
    }

    public async Task<List<TResult>> ListAsync<TResult>(
    Expression<Func<T, bool>>? predicate = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    Expression<Func<T, TResult>>? selector = null,
    int? skip = null,
    int? take = null,
    CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = context.Set<T>();

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
            query = orderBy(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        if (selector != null)
            return await query.Select(selector).ToListAsync(cancellationToken);
        else
            return await query.Cast<TResult>().ToListAsync(cancellationToken);
    }
}
