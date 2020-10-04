using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
    {
        private readonly ITodoItemRepository _repository;

        public UpdateTodoItemCommandHandler(
            ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(
            UpdateTodoItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            var requestTitle = request.Title;
            if (requestTitle != null)
            {
                entity.Title = request.Title;
            }

            if (request.Done == true)
            {
                entity.MarkComplete();
            }

            await _repository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}