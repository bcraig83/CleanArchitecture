using Domain.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
    {
        private readonly ITodoListRepository _repository;

        public UpdateTodoListCommandValidator(ITodoListRepository repository)
        {
            _repository = repository;

            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(ExistInRepository).WithMessage("No list found with specified id");

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public bool ExistInRepository(int id)
        {
            var allFromRepo =  _repository.GetAll()
                .Where(x => x.Id == id);

            if (allFromRepo.Count() == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> BeUniqueTitle(
            UpdateTodoListCommand model,
            string title,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAll()
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Title != title);
        }
    }
}