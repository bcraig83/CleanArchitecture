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
        private readonly ITodoItemRepository _repository;

        public DeleteTodoItemCommandHandler(
            ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            await _repository.RemoveAsync(entity);

            return Unit.Value;
        }
    }
}