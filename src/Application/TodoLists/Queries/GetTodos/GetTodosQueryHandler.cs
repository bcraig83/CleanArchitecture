using Application.TodoLists.Queries.GetTodos.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoLists.Queries.GetTodos
{
    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly ITodoListRepository _repository;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(
            IMapper mapper,
            ITodoListRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var priorityLevels = Enum.GetValues(typeof(PriorityLevel))
                    .Cast<PriorityLevel>()
                    .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                    .ToList();

            var lists = await _repository.GetAll()
                    .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken);

            var result = new TodosVm
            {
                PriorityLevels = priorityLevels,
                Lists = lists
            };

            return result;
        }
    }
}