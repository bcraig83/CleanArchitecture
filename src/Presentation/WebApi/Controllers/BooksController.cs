using Application.Books.Commands.CreateBook;
using Application.Books.Queries.GetBooks;
using Application.Books.Queries.GetBooks.Models;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class BooksController : ApiController
    {
        [HttpGet("health")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public ActionResult GetHealth()
        {
            return Ok("Books controller is up!");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<BookDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<BookDto>>> Get()
        {
            var result = await Mediator.Send(new GetBooksQuery());
            return Ok(result);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
             [FromBody] CreateBookCommand command)
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