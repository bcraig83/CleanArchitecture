using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly ITodoItemRepository _repository;

        public CreateTodoItemCommandHandler(
            ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(
            CreateTodoItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                ListId = request.ListId,
                Title = request.Title
            };

            var result = await _repository.AddAsync(entity);

            return result.Id;
        }
    }
}