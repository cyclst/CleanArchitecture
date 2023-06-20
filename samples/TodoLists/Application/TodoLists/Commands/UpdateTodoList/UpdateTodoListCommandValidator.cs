using TodoLists.Application.Persistence;
using FluentValidation;

namespace TodoLists.Application.TodoLists.Commands.UpdateTodoList;

public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
    private readonly ITodoListRepository _repository;

    public UpdateTodoListCommandValidator(ITodoListRepository repository)
    {
        _repository = repository;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
    {
        return await _repository.AllQueryAsync(l => l.Id == model.Id || l.Title != title);
    }
}
