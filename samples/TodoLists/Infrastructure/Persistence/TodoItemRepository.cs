using System.Linq.Expressions;
using TodoLists.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Cyclst.CleanArchitecture.Application.Persistence;
using TodoLists.Domain.Entities;
using Cyclst.CleanArchitecture.Application.Exceptions;
using TodoLists.Infrastructure.Persistence;

namespace TodoItems.Infrastructure.Persistence;
public class TodoItemRepository : ITodoItemRepository
{
    protected TodoListsDbContext Context;
    public IUnitOfWork UnitOfWork { get; }

    public TodoItemRepository(TodoListsDbContext context, IUnitOfWork unitOfWork)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await Context.TodoItems.FindAsync(id);
    }

    public IAsyncEnumerable<TodoItem> GetAllAsync()
    {
        return Context.TodoItems.AsAsyncEnumerable();
    }

    public async Task<bool> AllQueryAsync(Expression<Func<TodoItem, bool>> predicate)
    {
        return await Context.TodoItems.AsQueryable().AllAsync(predicate);
    }

    public async Task<bool> AnyQueryAsync(Expression<Func<TodoItem, bool>> predicate)
    {
        return await Context.TodoItems.AsQueryable().AnyAsync(predicate);
    }

    public async Task<int> AddAsync(TodoItem item)
    {
        await Context.TodoItems.AddAsync(item);

        return item.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);

        if (item is null)
            throw new NotFoundException(nameof(item), id);

        if (Context.Entry(item).State == EntityState.Detached)
        {
            Context.TodoItems.Attach(item);
        }

        Context.TodoItems.Remove(item);
    }

    public void Update(TodoItem item)
    {
        Context.TodoItems.Attach(item);
        Context.Entry(item).State = EntityState.Modified;
    }
}
