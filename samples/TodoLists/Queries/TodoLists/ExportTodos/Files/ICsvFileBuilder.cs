using TodoLists.Queries.TodoLists.ExportTodos;

namespace TodoLists.Queries.TodoLists.ExportTodos.Files;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
