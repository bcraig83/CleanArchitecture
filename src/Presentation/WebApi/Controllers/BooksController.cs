using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}