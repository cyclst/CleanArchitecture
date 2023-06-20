using TodoLists.Domain.Entities;
using MediatR;
using TodoLists.Application.Persistence;

namespace TodoLists.Application.TodoLists.Commands.DeleteTodoList;

public record DeleteTodoListCommand(int Id) : IRequest;

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly ITodoListRepository _todoListRepository;

    public DeleteTodoListCommandHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }

    public async Task Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        await _todoListRepository.DeleteAsync(request.Id);

        await _todoListRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
