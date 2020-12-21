using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        private IWebHostEnvironment _hostingEnvironment;

        private IWebHostEnvironment HostingEnvironment => _hostingEnvironment ??= (IWebHostEnvironment)HttpContext.RequestServices.GetService(typeof(IWebHostEnvironment));

        [HttpGet("env")]
        public IActionResult GetEnvironmentDetails()
        {
            var assembly = Assembly.GetExecutingAssembly().FullName;
            var result = new
            {
                Assembly = assembly,
                Environment = HostingEnvironment.EnvironmentName,
                MachineName = Environment.MachineName,
                OS = $"{RuntimeInformation.OSDescription} ({RuntimeInformation.OSArchitecture})"
            };
            return Ok(result);
        }
    }
}