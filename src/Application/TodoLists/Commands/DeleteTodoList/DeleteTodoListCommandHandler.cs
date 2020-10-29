using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoLists.Commands.DeleteTodoList
{
    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
    {
        private readonly ITodoListRepository _repository;

        public DeleteTodoListCommandHandler(
            ITodoListRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(
            DeleteTodoListCommand request,
            CancellationToken cancellationToken)
        {
            // Surely some of this defensive code could go inside the repository implementation?
            var entity = (await _repository.GetAllAsync())
                .Where(l => l.Id == request.Id)
                .SingleOrDefault();

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.Id);
            }

            await _repository.RemoveAsync(entity);

            return Unit.Value;
        }
    }
}