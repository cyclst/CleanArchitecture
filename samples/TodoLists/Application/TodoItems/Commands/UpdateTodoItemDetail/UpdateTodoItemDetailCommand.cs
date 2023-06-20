using TodoLists.Domain.Entities;
using TodoLists.Domain.Enums;
using MediatR;
using TodoLists.Application.Persistence;

namespace TodoLists.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly ITodoItemRepository _todoItemRepository;

    public UpdateTodoItemDetailCommandHandler(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoItemRepository.GetByIdAsync(request.Id);

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        _todoItemRepository.Update(entity);

        await _todoItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
