using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("accomodations")]
    public class AccomodationController : Controller
    {
        private readonly IAccomodationHandler handler;

        public AccomodationController(IAccomodationHandler handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccomodationModel accomodation)
        {
            return Ok(handler.Add(accomodation.ToEntity(), accomodation.TouristSpotId, accomodation.ImageNames));
        }

        [HttpPut]
        public IActionResult ChangeAvailability([FromBody] AccomodationModel accomodation, bool available)
        {
            return Ok(handler.ChangeAvailability(accomodation.ToEntity(), available));
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] AccomodationModel accomodation)
        {
            return Ok(handler.Delete(accomodation.ToEntity()));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] int accomodationId)
        {
            return Ok(handler.Get(accomodationId));
        }

        [HttpGet]
        public IActionResult GetByTouristSpot([FromBody] TouristSpotModel touristSpot, 
            DateTime checkIn, DateTime checkOut)
        {
            return Ok(handler.SearchByTouristSpot(touristSpot.ToEntity(), checkIn, checkOut));
        }
    }
}
