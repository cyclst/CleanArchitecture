using Cyclst.CleanArchitecture.Application.Persistence;
using TodoLists.Domain.Entities;

namespace TodoLists.Application.Persistence;

public interface ITodoItemRepository : IRepository<TodoItem>
{
    // Add repository specific methods here
}
