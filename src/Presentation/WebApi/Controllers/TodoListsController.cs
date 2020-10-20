using Application.TodoLists.Commands.CreateTodoList;
using Application.TodoLists.Commands.DeleteTodoList;
using Application.TodoLists.Commands.UpdateTodoList;
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

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromBody] UpdateTodoListCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoListCommand { Id = id });

            return NoContent();
        }
    }
}