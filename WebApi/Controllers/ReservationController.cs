using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationController : Controller
    {
        private readonly IReservationHandler handler;

        public ReservationController(IReservationHandler handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReservationModel reservation)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] int reservationId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult ChangeState([FromBody] int state, int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
