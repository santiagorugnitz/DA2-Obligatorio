﻿using System;
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
            handler.Add(accomodation.ToEntity(), accomodation.TouristSpotId, accomodation.ImageNames);
            return Ok("Accomodation added");
        }

        [HttpPut("{id}")]
        public IActionResult ChangeAvailability([FromHeader] int id, [FromBody] bool available)
        {
            handler.ChangeAvailability(id, available);
            return Ok("Availability changed");
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] AccomodationModel accomodation)
        {
            handler.Delete(accomodation.ToEntity());
            return Ok("Accomodation deleted");
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
