using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [Route("categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryHandler handler;


        public CategoryController(ICategoryHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Category> res = handler.GetAll();
            return Ok(res);
        }
    }
}
