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

    }
}
