using MyApp.Comments.Core.Domain;

namespace MyApp.Comments.Core.Application;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
