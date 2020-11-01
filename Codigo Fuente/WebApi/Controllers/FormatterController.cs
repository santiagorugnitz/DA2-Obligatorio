using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using DataImport;


namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [ServiceFilter(typeof(AuthorizationFilter))]
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
            return Ok(handler.GetAll());
        }

        [HttpGet("{id}/files")]
        public IActionResult GetFiles(int id)
        {
            return Ok(handler.GetFileNames(id));
        }

        [HttpGet("{id}/load")]
        public IActionResult LoadFile(int id, List<SourceParameter> parameters)
        {
            return Ok(handler.Add(id,parameters));
        }

    }
}
