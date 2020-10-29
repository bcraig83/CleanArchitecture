﻿using Domain.Repositories;
using FluentValidation;
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
                .MustAsync(ExistInRepository).WithMessage("No list found with specified id");

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> ExistInRepository(int id, CancellationToken cancellationToken)
        {
            return (await _repository.GetAllAsync())
                .Any(x => x.Id == id);
        }

        public async Task<bool> BeUniqueTitle(
            UpdateTodoListCommand model,
            string title,
            CancellationToken cancellationToken)
        {
            return (await _repository.GetAllAsync())
                .Where(l => l.Id != model.Id)
                .All(l => l.Title != title);
        }
    }
}