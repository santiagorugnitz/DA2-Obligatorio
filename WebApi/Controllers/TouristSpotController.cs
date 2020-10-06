using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
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
        public IActionResult Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(handler.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] TouristSpotModel spot)
        {
            return null;

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TouristSpotModel spotModel)
        {
            var spot = BuildTouristSpot(spotModel);

            return Ok(handler.Add(spot));
        }

        private TouristSpot BuildTouristSpot(TouristSpotModel spotModel)
        {
            var image = new Image()
            {
                Id = 1,
                Name = "image"
            };
            var category = new Category
            {
                Id = 1,
                Name = "Campo"
            };
            var region = new Region() { Name = "Region metropolitana" };


            return new TouristSpot()
            {
                Id = 1,
                Name = "Beach",
                Description = "asd",
                Image = image,
                Region = region,
                TouristSpotCategories = new List<TouristSpotCategory> { new TouristSpotCategory() { Category = category } }
            };
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return null;
        }
    }
}
