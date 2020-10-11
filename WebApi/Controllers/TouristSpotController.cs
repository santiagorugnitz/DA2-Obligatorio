using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using WebApi.Filters;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [Route("spots")]
    [ApiController]
    public class TouristSpotController : ControllerBase
    {

        private readonly ITouristSpotHandler handler;


        public TouristSpotController(ITouristSpotHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public IActionResult GetAll(int region, [FromQuery] List<int> cat)
        {
            return Ok(handler.Search(cat,region));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = handler.Get(id);
            if (null == res) throw new NotFoundException("The Tourist Spot does not exists");
            return Ok(res);
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]
        public IActionResult Post([FromBody] TouristSpotModel spot)
        {
            handler.Add(spot.ToEntity(),spot.RegionId,spot.CategoryIds,spot.Image);
            return Ok("Tourist spot added");

        }
    }
}
