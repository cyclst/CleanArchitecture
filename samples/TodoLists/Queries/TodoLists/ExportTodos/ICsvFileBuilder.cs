using TodoLists.Queries.TodoLists.ExportTodos;

namespace TodoLists.Queries.TodoItems.GetTodoItemsWithPagination;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
