using System.Globalization;
using TodoLists.Infrastructure.Files.Maps;
using CsvHelper;
using TodoLists.Queries.TodoItems.GetTodoItemsWithPagination;
using TodoLists.Queries.TodoLists.ExportTodos;

namespace TodoLists.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
