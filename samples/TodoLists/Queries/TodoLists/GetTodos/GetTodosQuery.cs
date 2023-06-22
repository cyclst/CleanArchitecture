using Cyclst.CleanArchitecture.Application.Security;
using TodoLists.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mapster;
using TodoLists.Infrastructure.Persistence;

namespace TodoLists.Queries.TodoLists.GetTodos;

[Authorize]
public record GetTodosQuery : IRequest<TodosVm>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
    private readonly TodoListsDbContext _context;

    public GetTodosQueryHandler(TodoListsDbContext context)
    {
        _context = context;
    }

    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList(),

            Lists = await _context.TodoLists
                .AsNoTracking()
                .ProjectToType<TodoListDto>()
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken)
        };
    }
}
