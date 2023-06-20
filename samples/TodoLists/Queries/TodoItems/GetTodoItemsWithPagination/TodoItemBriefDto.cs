using CleanArchitecture.Application.Mappings;
using TodoLists.Domain.Entities;

namespace TodoLists.Queries.TodoItems.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}
