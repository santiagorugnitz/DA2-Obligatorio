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
    [Route("importers")]
    [ApiController]
    public class ImporterController : ControllerBase
    {
        private readonly IImporterHandler handler;
        public ImporterController(IImporterHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(handler.GetAll());
        }

        [HttpPost("{id}/upload")]
        public IActionResult Upload(int id, List<SourceParameter> parameters)
        {
            return Ok(handler.Add(id,parameters));
        }

    }
}
