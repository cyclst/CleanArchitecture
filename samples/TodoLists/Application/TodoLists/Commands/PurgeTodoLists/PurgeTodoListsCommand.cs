using CleanArchitecture.Application.Security;
using MediatR;
using TodoLists.Application.Persistence;

namespace TodoLists.Application.TodoLists.Commands.PurgeTodoLists;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public record PurgeTodoListsCommand : IRequest;

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
{
    private readonly ITodoListRepository _todoListRepository;

    public PurgeTodoListsCommandHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }

    public async Task Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        var entities = _todoListRepository.GetAllAsync();

        await foreach(var entity in entities)
        {
            await _todoListRepository.DeleteAsync(entity.Id);
        }

        await _todoListRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
