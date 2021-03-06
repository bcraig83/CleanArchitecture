﻿using Application.Common.Interfaces;
using Application.TodoLists.Queries.ExportTodos.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoLists.Queries.ExportTodos
{
    public class ExportTodosQueryHandler : IRequestHandler<ExportTodosQuery, ExportTodosVm>
    {
        private readonly IRepository<TodoItem> _repository;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportTodosQueryHandler(
            IMapper mapper,
            ICsvFileBuilder fileBuilder,
            IRepository<TodoItem> repository)
        {
            _mapper = mapper;
            _fileBuilder = fileBuilder;
            _repository = repository;
        }

        public async Task<ExportTodosVm> Handle(
            ExportTodosQuery request,
            CancellationToken cancellationToken)
        {
            var vm = new ExportTodosVm();

            var records = (await _repository.GetAllAsync())
                    .Where(t => t.ListId == request.ListId)
                    .AsQueryable()
                    .ProjectTo<TodoItemRecord>(_mapper.ConfigurationProvider)
                    .ToList();

            vm.Content = _fileBuilder.BuildTodoItemsFile(records);
            vm.ContentType = "text/csv";
            vm.FileName = "TodoItems.csv";

            return await Task.FromResult(vm);
        }
    }
}