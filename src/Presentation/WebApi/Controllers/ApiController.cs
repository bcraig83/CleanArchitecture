using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        [HttpGet("env")]
        public IActionResult GetEnvironmentDetails()
        {
            var assembly = Assembly.GetExecutingAssembly().FullName;
            var result = new
            {
                Assembly = assembly,
                Environment = _hostingEnvironment.EnvironmentName,
                MachineName = Environment.MachineName,
                OS = $"{RuntimeInformation.OSDescription} ({RuntimeInformation.OSArchitecture})"
            };
            return Ok(result);
        }
    }
}