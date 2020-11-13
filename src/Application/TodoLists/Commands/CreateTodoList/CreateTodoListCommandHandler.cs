using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
    {
        private readonly IRepository<TodoList> _repository;

        public CreateTodoListCommandHandler(
            IRepository<TodoList> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList
            {
                Title = request.Title
            };

            var addedEntity = await _repository.AddAsync(entity);

            return addedEntity.Id;
        }
    }
}