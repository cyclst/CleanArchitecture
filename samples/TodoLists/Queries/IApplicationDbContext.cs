using TodoLists.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.EntityFramework;

namespace TodoLists.Queries;

public interface ITodoListsDbContext : IDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
}
