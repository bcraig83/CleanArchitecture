using FluentValidation;

namespace Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200).WithMessage("Max length of title is 200 characters")
                .NotEmpty().WithMessage("Title field cannot be empty");
        }
    }
}