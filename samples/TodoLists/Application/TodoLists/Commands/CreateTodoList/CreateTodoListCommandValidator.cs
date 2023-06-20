using TodoLists.Application.Persistence;
using FluentValidation;

namespace TodoLists.Application.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    private readonly ITodoListRepository _repository;

    public CreateTodoListCommandValidator(ITodoListRepository repository)
    {
        _repository = repository;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _repository.AllQueryAsync(l => l.Title != title);
    }
}
