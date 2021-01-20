using Application.Books.Commands.CreateBook;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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