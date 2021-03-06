﻿using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        private readonly IRepository<TodoList> _repository;

        public CreateTodoListCommandValidator(
            IRepository<TodoList> repository)
        {
            _repository = repository;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return (await _repository.GetAllAsync())
                .All(l => l.Title != title);
        }
    }
}