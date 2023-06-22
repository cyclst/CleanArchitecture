using Cyclst.CleanArchitecture.Application.Exceptions;
using TodoLists.Domain.Entities;
using TodoLists.Domain.Events;
using MediatR;
using TodoLists.Application.Persistence;

namespace TodoLists.Application.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly ITodoItemRepository _todoItemRepository;

    public DeleteTodoItemCommandHandler(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        await _todoItemRepository.DeleteAsync(request.Id);

        await _todoItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

}
