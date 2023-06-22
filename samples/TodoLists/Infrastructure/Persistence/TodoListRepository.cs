using System.Linq.Expressions;
using TodoLists.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Cyclst.CleanArchitecture.Application.Persistence;
using TodoLists.Domain.Entities;
using Cyclst.CleanArchitecture.Application.Exceptions;

namespace TodoLists.Infrastructure.Persistence;
public class TodoListRepository : ITodoListRepository
{
    protected TodoListsDbContext Context;
    public IUnitOfWork UnitOfWork { get; }

    public TodoListRepository(TodoListsDbContext context, IUnitOfWork unitOfWork)
    {
        Context = context;
        UnitOfWork = unitOfWork;
    }

    public async Task<TodoList> GetByIdAsync(int id)
    {
        return await Context.TodoLists.FindAsync(id);
    }

    public IAsyncEnumerable<TodoList> GetAllAsync()
    {
        return Context.TodoLists.AsAsyncEnumerable();
    }

    public async Task<bool> AllQueryAsync(Expression<Func<TodoList, bool>> predicate)
    {
        return await Context.TodoLists.AsQueryable().AllAsync(predicate);
    }

    public async Task<bool> AnyQueryAsync(Expression<Func<TodoList, bool>> predicate)
    {
        return await Context.TodoLists.AsQueryable().AnyAsync(predicate);
    }

    public async Task<int> AddAsync(TodoList item)
    {
        await Context.TodoLists.AddAsync(item);

        return item.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);

        if (item is null)
            throw new NotFoundException(nameof(TodoList), id);

        if (Context.Entry(item).State == EntityState.Detached)
        {
            Context.TodoLists.Attach(item);
        }

        Context.TodoLists.Remove(item);
    }

    public void Update(TodoList item)
    {
        Context.TodoLists.Attach(item);
        Context.Entry(item).State = EntityState.Modified;
    }
}
