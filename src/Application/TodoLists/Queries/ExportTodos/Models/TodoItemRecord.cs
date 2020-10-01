using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.TodoLists.Queries.ExportTodos.Models
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoItemRecord>()
                .ForMember(d => d.Done, opt => opt.MapFrom(s => s.IsDone));
        }
    }
}