using CleanArchitecture.Application.Exceptions;
using TodoLists.Domain.Entities;
using MediatR;
using TodoLists.Application.Persistence;

namespace TodoLists.Application.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
{
    private readonly ITodoListRepository _todoListRepository;

    public UpdateTodoListCommandHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }

    public async Task Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoListRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        entity.Title = request.Title;

        _todoListRepository.Update(entity);

        await _todoListRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

    }
}
