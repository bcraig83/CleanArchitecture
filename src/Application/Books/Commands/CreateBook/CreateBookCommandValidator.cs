using FluentValidation;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Cannot create a book without a title");

            // TODO: write a test for this!
            RuleFor(v => v.ISBN10)
                .Matches("^[0-9]{10}$")
                .WithMessage("ISBN10 code must be made up of 10 digits");
        }
    }
}