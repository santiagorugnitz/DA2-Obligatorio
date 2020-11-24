using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [ApiController]
    [Route("accommodations")]
    public class accommodationController : Controller
    {
        private readonly IaccommodationHandler handler;

        public accommodationController(IaccommodationHandler handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        public IActionResult Post([FromBody] accommodationModel accommodation)
        {
            handler.Add(accommodation.ToEntity(), accommodation.TouristSpotId, accommodation.ImageNames);
            return Ok("accommodation added");
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult ChangeAvailability( int id, [FromBody] bool available)
        {
            handler.ChangeAvailability(id, available);
            return Ok("Availability changed");
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpDelete("{id}")]
        public IActionResult Delete( int id)
        {
            handler.Delete(id);
            return Ok("accommodation deleted");
        }

        [HttpGet("{id}")]
        public IActionResult Get( int id)
        {
            var res = handler.Get(id);
            if (null == res) throw new NotFoundException("The accommodation does not exist");
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetByTouristSpot(int spotId,bool onlyAvailable)
        {
            return Ok(handler.SearchByTouristSpot(spotId));
        }

        [HttpPost("{id}/calculateTotal")]
        public IActionResult CalculateTotal(int id, [FromBody] StayModel stay)
        {
            return Ok(handler.CalculateTotal(id,stay.ToEntity()));
        }
    }
}
