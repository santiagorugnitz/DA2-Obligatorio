using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("regions")]
    [ApiController]
    public class RegionController : ControllerBase
    {

        private readonly IRegionHandler handler;


        public RegionController(IRegionHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Region> res = handler.GetAll();
            return Ok(res);
        }
    }
}
