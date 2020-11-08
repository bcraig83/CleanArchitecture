using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly IRepository<TodoItem> _repository;

        public DeleteTodoItemCommandHandler(
            IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            await _repository.RemoveAsync(entity);

            return Unit.Value;
        }
    }
}