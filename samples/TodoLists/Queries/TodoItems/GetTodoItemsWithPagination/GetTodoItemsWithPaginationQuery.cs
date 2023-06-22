using MediatR;
using Cyclst.CleanArchitecture.EntityFramework;
using Mapster;
using TodoLists.Infrastructure.Persistence;

namespace TodoLists.Queries.TodoItems.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
    private readonly TodoListsDbContext _context;

    public GetTodoItemsWithPaginationQueryHandler(TodoListsDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .Where(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .ProjectToType<TodoItemBriefDto>()
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
