using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cyclst.CleanArchitecture.EntityFramework;

public interface IDbContext
{
    ChangeTracker ChangeTracker { get; }

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
    where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
