using Comments.SharedKernel;

namespace Comments.Application;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : EntityBase;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
