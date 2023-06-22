using Cyclst.CleanArchitecture.Application.Exceptions;
using TodoLists.Domain.Entities;
using MediatR;
using TodoLists.Application.Persistence;

namespace TodoLists.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly ITodoItemRepository _todoItemRepository;

    public UpdateTodoItemCommandHandler(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoItemRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        entity.Title = request.Title;
        entity.Done = request.Done;

        _todoItemRepository.Update(entity);

        await _todoItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
