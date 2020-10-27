using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [Route("formatters")]
    [ApiController]
    public class FormatterController : ControllerBase
    {
        private readonly IFormatterHandler handler;


        public FormatterController(IFormatterHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost("{id}")]
        public IActionResult ReadFile()
        {
            throw new NotImplementedException();
        }

    }
}
