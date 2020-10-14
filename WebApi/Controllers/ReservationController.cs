using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
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
            var res = handler.Add(reservation.ToEntity(), reservation.AccomodationId);
            return Ok(res.Id);

        }

        [HttpGet("{id}")]
        public IActionResult CheckState(int id)
        {
            var res = handler.CheckState(id);
            if (null == res) throw new NotFoundException("The reservation does not exist");
            return Ok(res);
        }

        [HttpGet()]
        public IActionResult GetFromAccomodation(int accomodationId)
        {
            return Ok(handler.GetAllFromAccomodation(accomodationId));
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult ChangeState(int id, [FromBody] ReservationChangeModel change)
        {
            handler.ChangeState(id, change.State, change.Description);
            return Ok("Reservation state updated");
        }
    }
}
