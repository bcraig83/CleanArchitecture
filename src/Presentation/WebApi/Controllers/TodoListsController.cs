using Application.TodoLists.Commands.CreateTodoList;
using Application.TodoLists.Queries.GetTodos;
using Application.TodoLists.Queries.GetTodos.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
             [FromBody] CreateTodoListCommand command)
        {
            int result;
            
            try
            {
                result = await Mediator.Send(command);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Message);
            }

            return Ok(result);
        }
    }
}