using TodoLists.Domain.Entities;
using MediatR;

using TodoLists.Application.Persistence;

namespace TodoLists.Application.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly ITodoListRepository _todoListRepository;

    public CreateTodoListCommandHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.Title;

        await _todoListRepository.AddAsync(entity);

        await _todoListRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
