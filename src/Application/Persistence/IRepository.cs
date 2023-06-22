using System.Linq.Expressions;
using Cyclst.CleanArchitecture.Domain;

namespace Cyclst.CleanArchitecture.Application.Persistence;

public interface IRepository<T> where T : BaseEntity
{
    IUnitOfWork UnitOfWork { get; }

    Task<T> GetByIdAsync(int id);

    IAsyncEnumerable<T> GetAllAsync();

    Task<bool> AllQueryAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AnyQueryAsync(Expression<Func<T, bool>> predicate);

    Task<int> AddAsync(T entity);

    Task DeleteAsync(int id);

    void Update(T entity);
}
