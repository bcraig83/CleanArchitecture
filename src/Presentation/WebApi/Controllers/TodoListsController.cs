using Application.TodoLists.Commands.CreateTodoList;
using Application.TodoLists.Queries.GetTodos;
using Application.TodoLists.Queries.GetTodos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class TodoListsController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(TodosVm), StatusCodes.Status200OK)]
        public async Task<ActionResult<TodosVm>> Get()
        {
            return await Mediator.Send(new GetTodosQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(
             [FromBody] CreateTodoListCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}