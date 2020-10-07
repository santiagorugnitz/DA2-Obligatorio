using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

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
        public IActionResult Post([FromBody] ReservationModel reservation, int accomodationId)
        {
            return Ok(handler.Add(reservation.ToEntity(), accomodationId));
        }

        [HttpGet("{id}")]
        public IActionResult CheckState([FromHeader] int reservationId)
        {
            return Ok(handler.CheckState(reservationId));
        }

        [HttpPut]
        public IActionResult ChangeState([FromBody] ReservationState state, int reservationId, string description)
        {
            return Ok(handler.ChangeState(reservationId, state, description));
        }
    }
}
