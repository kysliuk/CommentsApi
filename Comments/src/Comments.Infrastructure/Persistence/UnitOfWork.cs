using Comments.Application;
using Comments.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Res = Comments.Infrastructure.Properties.Resources;

namespace Comments.Infrastructure;

public class UnitOfWork(DbContext context) : IUnitOfWork
{
    private readonly Hashtable _repositories = [];

    public IRepository<T> Repository<T>() where T : EntityBase
    {
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IRepository<T>)_repositories[type]!;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format(Res.UnitOfWork_AnErrorOccurredWhileSavingChanges0, ex.Message));
            throw;
        }
    }
}
