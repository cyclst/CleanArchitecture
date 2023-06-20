using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoLists.Queries.TodoItems.GetTodoItemsWithPagination;

namespace TodoLists.Queries.TodoLists.ExportTodos;

public record ExportTodosQuery : IRequest<ExportTodosVm>
{
    public int ListId { get; init; }
}

public class ExportTodosQueryHandler : IRequestHandler<ExportTodosQuery, ExportTodosVm>
{
    private readonly ITodoListsDbContext _context;
    private readonly ICsvFileBuilder _fileBuilder;

    public ExportTodosQueryHandler(ITodoListsDbContext context, ICsvFileBuilder fileBuilder)
    {
        _context = context;
        _fileBuilder = fileBuilder;
    }

    public async Task<ExportTodosVm> Handle(ExportTodosQuery request, CancellationToken cancellationToken)
    {
        var records = await _context.TodoItems
                .Where(t => t.ListId == request.ListId)
                .ProjectToType<TodoItemRecord>()
                .ToListAsync(cancellationToken);

        var vm = new ExportTodosVm(
            "TodoItems.csv",
            "text/csv",
            _fileBuilder.BuildTodoItemsFile(records));

        return vm;
    }
}