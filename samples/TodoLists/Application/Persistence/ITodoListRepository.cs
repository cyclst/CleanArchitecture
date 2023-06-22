using Cyclst.CleanArchitecture.Application.Persistence;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.Persistence;

public interface ITodoListRepository : IRepository<TodoList>
{
    // Add repository specific methods here
}
