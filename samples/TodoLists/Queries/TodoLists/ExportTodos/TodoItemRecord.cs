using CleanArchitecture.Application.Mappings;
using TodoLists.Domain.Entities;

namespace TodoLists.Queries.TodoLists.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
